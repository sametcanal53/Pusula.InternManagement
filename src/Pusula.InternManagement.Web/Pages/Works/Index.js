$(function () {
    var l = abp.localization.getResource('InternManagement');
    var createModal = new abp.ModalManager(abp.appPath + 'Works/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Works/EditModal');

    var dataTable = $('#WorksTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[4, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(pusula.internManagement.works.work.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('InternManagement.Works.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('InternManagement.Works.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'WorkDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        pusula.internManagement.works.work
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
                    visible: abp.auth.isGranted('InternManagement.Works.Admin')
                },
                {
                    title: l('InternId'),
                    data: "internId",
                    visible: abp.auth.isGranted('InternManagement.Works.Admin')
                },
                {
                    title: l('InternName'),
                    data: "internName",
                    visible: abp.auth.isGranted('InternManagement.Works.Admin')
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Description'),
                    data: "description"
                },
                {
                    title: l('Date'),
                    data: "date",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LL');
                    }
                },
                {
                    title: l('CreatorId'),
                    data: "creatorId",
                    visible: abp.auth.isGranted('InternManagement.Works.Admin'),
                },
                {
                    title: l('CreationTime'),
                    data: "creationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Works.Admin')
                },
                {
                    title: l('LastModifierId'),
                    data: "lastModifierId",
                    visible: abp.auth.isGranted('InternManagement.Works.Admin'),
                },
                {
                    title: l('LastModificationTime'),
                    data: "lastModificationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Works.Admin'),
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

    $('#NewWorkButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
