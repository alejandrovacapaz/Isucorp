function GotoIndex() {
    window.location.href = "/Home/Index";
}

function toDate(dateStr) { //MM/DD/YYYY
    var parts = dateStr.split("/");
    return new Date(parts[2], parts[0] - 1, parts[1]);
}

function DatetoString(date) { //MM/DD/YYYY
    return ((date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear());
}

function prepareModal(e, modalName) {
    e.preventDefault();
    $('#' + modalName).modal('show');
}

