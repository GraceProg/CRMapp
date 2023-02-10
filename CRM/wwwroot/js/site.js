
$(document).ready(function () {
    $('#table').DataTable({
        "ordering": true,
        "columnDefs": [{
            "targets": [1, 2, 3, 4],
            "ordorable": false
        }]
    });
});



function formatDate(date, includeTime = false) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var dateString = `${year}-${month.toLocaleString(undefined, { minimumIntegerDigits: 2 })}-${day.toLocaleString(undefined, { minimumIntegerDigits: 2 })}`;

    if (includeTime) {
        var hour = date.getHours().toLocaleString(undefined, { minimumIntegerDigits: 2 });
        var minute = date.getMinutes().toLocaleString(undefined, { minimumIntegerDigits: 2 });
        dateString += ` ${hour}:${minute}:00`;
    }

    return dateString;
}
