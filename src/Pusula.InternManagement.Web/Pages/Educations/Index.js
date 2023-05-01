$(function () {
    var l = abp.localization.getResource('InternManagement');
    var createModal = new abp.ModalManager(abp.appPath + 'Educations/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Educations/EditModal');

    var dataTable = $('#EducationsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[2, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(pusula.internManagement.educations.education.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('InternManagement.Educations.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('InternManagement.Educations.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'EducationDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        pusula.internManagement.educations.education
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
                    visible: abp.auth.isGranted('InternManagement.Educations.Admin'),
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('InternId'),
                    data: "internId",
                    visible: abp.auth.isGranted('InternManagement.Educations.Admin'),
                },
                {
                    title: l('InternName'),
                    data: "internName"
                },
                {
                    title: l('UniversityId'),
                    data: "universityId",
                    visible: abp.auth.isGranted('InternManagement.Educations.Admin'),
                },
                {
                    title: l('UniversityName'),
                    data: "universityName"
                },
                {
                    title: l('UniversityDepartmentId'),
                    data: "universityDepartmentId",
                    visible: abp.auth.isGranted('InternManagement.Educations.Admin'),
                },
                {
                    title: l('UniversityDepartmentName'),
                    data: "universityDepartmentName"
                },
                {
                    title: l('Grade'),
                    data: "grade",
                    render: function (data) {
                        return l('Grade.' + data);
                    }
                },
                {
                    title: l('GradePointAverage'),
                    data: "gradePointAverage"
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
                    visible: abp.auth.isGranted('InternManagement.Educations.Admin'),
                },
                {
                    title: l('CreationTime'),
                    data: "creationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Educations.Admin')
                },
                {
                    title: l('LastModifierId'),
                    data: "lastModifierId",
                    visible: abp.auth.isGranted('InternManagement.Educations.Admin'),
                },
                {
                    title: l('LastModificationTime'),
                    data: "lastModificationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Educations.Admin'),
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

    $('#NewEducationButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
