function downloadFile(internId, fileName) {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/api/app/file/download/' + internId + '/' + fileName, true);
    xhr.responseType = 'blob';
    xhr.onload = function () {
        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(xhr.response);
        link.download = fileName;
        link.click();
    };
    xhr.send();
}
