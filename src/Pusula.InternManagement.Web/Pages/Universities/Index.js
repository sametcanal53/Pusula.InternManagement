$(function () {
    var l = abp.localization.getResource('InternManagement');
    var createModal = new abp.ModalManager(abp.appPath + 'Universities/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Universities/EditModal');

    var dataTable = $('#UniversitiesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[2, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(pusula.internManagement.universities.university.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('InternManagement.Universities.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('InternManagement.Universities.Delete'),
                                    confirmMessage: function (data) {
                                        return l('UniversityDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        pusula.internManagement.universities.university
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
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
                    visible: abp.auth.isGranted('InternManagement.Universities.Admin'),
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('CreatorId'),
                    data: "creatorId",
                    visible: abp.auth.isGranted('InternManagement.Universities.Admin'),
                },
                {
                    title: l('CreationTime'),
                    data: "creationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Universities.Admin'),
                },
                {
                    title: l('LastModifierId'),
                    data: "lastModifierId",
                    visible: abp.auth.isGranted('InternManagement.Universities.Admin'),
                },
                {
                    title: l('LastModificationTime'),
                    data: "lastModificationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Universities.Admin'),
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

    $('#NewUniversityButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});