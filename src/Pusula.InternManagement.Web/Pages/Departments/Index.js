$(function () {
    var l = abp.localization.getResource('InternManagement');
    var createModal = new abp.ModalManager(abp.appPath + 'Departments/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Departments/EditModal');

    var dataTable = $('#DepartmentsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[2, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(pusula.internManagement.departments.department.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('InternManagement.Departments.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('InternManagement.Departments.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'DepartmentDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        pusula.internManagement.departments.department
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Id'),
                    data: "id",
                    visible: abp.auth.isGranted('InternManagement.Departments.Admin'),
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('CreatorId'),
                    data: "creatorId",
                    visible: abp.auth.isGranted('InternManagement.Departments.Admin'),
                },
                {
                    title: l('CreationTime'),
                    data: "creationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Departments.Admin'),
                },
                {
                    title: l('LastModifierId'),
                    data: "lastModifierId",
                    visible: abp.auth.isGranted('InternManagement.Departments.Admin'),
                },
                {
                    title: l('LastModificationTime'),
                    data: "lastModificationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Departments.Admin'),
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewDepartmentButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
