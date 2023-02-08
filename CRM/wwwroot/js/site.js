
$(document).ready(function () {
    $('#table').DataTable({
        "ordering": true,
        "columnDefs": [{
            "targets": [1, 2, 3, 4],
            "ordorable": false
        }]
    });
});

