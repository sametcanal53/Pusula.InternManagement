$(function () {
    var l = abp.localization.getResource('InternManagement');
    var createModal = new abp.ModalManager(abp.appPath + 'Experiences/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Experiences/EditModal');

    var dataTable = $('#ExperiencesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[2, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(pusula.internManagement.experiences.experience.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('InternManagement.Experiences.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('InternManagement.Experiences.Delete'),
                                    confirmMessage: function (data) {
                                        return l('ExperienceDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        pusula.internManagement.experiences.experience
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
                    visible: abp.auth.isGranted('InternManagement.Experiences.Admin')
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('InternId'),
                    data: "internId",
                    visible: abp.auth.isGranted('InternManagement.Experiences.Admin')
                },
                {
                    title: l('InternName'),
                    data: "internName",
                    visible: abp.auth.isGranted('InternManagement.Educations.Admin')
                },
                {
                    title: l('Title'),
                    data: "title"
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
                    title: l('CompanyName'),
                    data: "companyName"
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
                    title: l('CreatorId'),
                    data: "creatorId",
                    visible: abp.auth.isGranted('InternManagement.Experiences.Admin'),
                },
                {
                    title: l('CreationTime'),
                    data: "creationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Experiences.Admin'),
                },
                {
                    title: l('LastModifierId'),
                    data: "lastModifierId",
                    visible: abp.auth.isGranted('InternManagement.Experiences.Admin'),
                },
                {
                    title: l('LastModificationTime'),
                    data: "lastModificationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Experiences.Admin'),
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

    $('#NewExperienceButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});