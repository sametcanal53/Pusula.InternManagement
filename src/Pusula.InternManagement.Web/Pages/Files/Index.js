$(function () {
    var l = abp.localization.getResource('InternManagement');
    var createModal = new abp.ModalManager(abp.appPath + 'Files/CreateModal');

    var dataTable = $('#FilesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(pusula.internManagement.files.file.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Download'),
                                    /*action: function (data) {
                                        var fileName = data.record.name.split("/")[1];
                                        window.location.href = '/download/' + fileName;
                                    }*/
                                    // Using the Fetch API for downloading is more secure
                                    action: function (data) {
                                        var fileName = data.record.name;
                                        var internId = data.record.internId;
                                        fetch('/download/' + internId + "/" + fileName)
                                            .then(response => response.blob())
                                            .then(blob => {
                                                var url = window.URL.createObjectURL(blob);
                                                var a = document.createElement('a');
                                                a.href = url;
                                                a.download = fileName;
                                                document.body.appendChild(a);
                                                a.click();
                                                a.remove();
                                            });
                                    }

                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('InternManagement.Files.Delete'),
                                    confirmMessage: function (data) {
                                        return l('FileDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        pusula.internManagement.files.file
                                            .delete(data.record.internId, data.record.name)
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
                    visible: abp.auth.isGranted('InternManagement.Files.Admin')
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('InternId'),
                    data: "internId",
                    visible: abp.auth.isGranted('InternManagement.Files.Admin')
                },
                {
                    title: l('Intern'),
                    data: "internName",
                    visible: abp.auth.isGranted('InternManagement.Files.Admin')
                },
                {
                    title: l('CreatorId'),
                    data: "creatorId",
                    visible: abp.auth.isGranted('InternManagement.Files.Admin')
                },
                {
                    title: l('CreationTime'),
                    data: "creationTime",
                    render: function (data) {
                        return moment(data).format('LLL');
                    },
                    visible: abp.auth.isGranted('InternManagement.Files.Admin')
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewFileButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});