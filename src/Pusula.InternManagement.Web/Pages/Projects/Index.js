$(function () {
    var l = abp.localization.getResource('InternManagement');
    var createModal = new abp.ModalManager(abp.appPath + 'Projects/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Projects/EditModal');

    var dataTable = $('#ProjectsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[2, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(pusula.internManagement.projects.project.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('InternManagement.Projects.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('InternManagement.Projects.Delete'),
                                    confirmMessage: function (data) {
                                        return l('ProjectDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        pusula.internManagement.projects.project
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
                    visible: abp.auth.isGranted('InternManagement.Projects.Admin'),
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Description'),
                    data: "description",
                    render: function (data) {
                        if (data.length >= 40)
                            return data.slice(0, 40) + " ...";
                        else
                            return data;

                    }
                },
                {
                    title: l('StartDate'),
                    data: "startDate",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LL');
                    }
                },
                {
                    title: l('EndDate'),
                    data: "endDate",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LL');
                    }
                },
                {
                    title: l("Interns"),
                    data: "interns",
                    render: function (data) {
                        data.sort();
                        return data.join(" | ");
                    }
                },
                {
                    title: l('CreatorId'),
                    data: "creatorId",
                    visible: abp.auth.isGranted('InternManagement.Projects.Admin')
                },
                {
                    title: l('CreationTime'),
                    data: "creationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Projects.Admin')
                },
                {
                    title: l('LastModifierId'),
                    data: "lastModifierId",
                    visible: abp.auth.isGranted('InternManagement.Projects.Admin')
                },
                {
                    title: l('LastModificationTime'),
                    data: "lastModificationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Projects.Admin')
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

    $('#NewProjectButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});