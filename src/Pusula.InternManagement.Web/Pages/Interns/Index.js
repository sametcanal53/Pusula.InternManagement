

$(function () {
    var l = abp.localization.getResource('InternManagement');
    var createModal = new abp.ModalManager(abp.appPath + 'Interns/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Interns/EditModal');

    var dataTable = $('#InternsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[2, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(pusula.internManagement.interns.intern.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible: abp.auth.isGranted('InternManagement.Interns.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Detail'),
                                    visible: abp.auth.isGranted('InternManagement.Interns.Admin'),
                                    action: function (data) {
                                        window.open('/Interns/Intern?id=' + data.record.id, "_blank");
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('InternManagement.Interns.Delete'),
                                    confirmMessage: function (data) {
                                        return l('InternDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        pusula.internManagement.interns.intern
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
                    visible: abp.auth.isGranted('InternManagement.Interns.Admin'),
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('DepartmentId'),
                    data: "departmentId",
                    visible: abp.auth.isGranted('InternManagement.Interns.Admin'),
                },
                {
                    title: l('Department'),
                    data: "departmentName"
                },
                {
                    title: l('PhoneNumber'),
                    data: "phoneNumber"
                },
                {
                    title: l('Email'),
                    data: "email"
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
                    title: l('TotalInternshipDays'),
                    data: null,
                    render: function (data, type, row) {
                        var date1 = moment(row.startDate, "YYYY-MM-DD");
                        var date2 = moment(row.endDate, "YYYY-MM-DD");
                        var diff = date2.diff(date1, "days") + 1;
                        return diff + l('day');
                    }
                },
                {
                    title: l('RemainingInternshipDays'),
                    data: null,
                    render: function (data, type, row) {
                        var date1 = moment(row.endDate, "YYYY-MM-DD");
                        var date2 = new Date();
                        var diff = moment(date1).diff(date2, "days") + 1;
                        if (diff <= 0) {
                            return l('InternshipCompleted');
                        }
                        return diff + l('day');
                    }
                },
                {
                    title: l('CreatorId'),
                    data: "creatorId",
                    visible: abp.auth.isGranted('InternManagement.Interns.Admin'),
                },
                {
                    title: l('CreationTime'),
                    data: "creationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Interns.Admin'),
                },
                {
                    title: l('LastModifierId'),
                    data: "lastModifierId",
                    visible: abp.auth.isGranted('InternManagement.Interns.Admin'),
                },
                {
                    title: l('LastModificationTime'),
                    data: "lastModificationTime",
                    render: function (data) {
                        if (data == null)
                            return null;
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Interns.Admin'),
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

    $('#NewInternButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});