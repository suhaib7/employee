var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": '/section/getall',
            "dataSrc": "data"
        },
        "columns": [
            { "data": "nameEN", "width": "25%" },
            { "data": "nameAR", "width": "25%" },
            {
                "data": function (row) {
                    return row.departmentNameEN + ' / ' + row.departmentNameAR;
                },
                "width": "25%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                                <a onClick="Delete('/section/delete/${data}')" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete </a>
                                            </div>`;
                },
                "width": "25%"
            }
        ],
        "language": {
            "emptyTable": "No sections available"
        },
        "width": "100%"
    });
}



function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (xhr, status, error) {
                    toastr.error('An error occurred while deleting the section: ' + error);
                }
            });
        }
    });
}
