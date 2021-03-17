
var dataTable;
$(document).ready(function () {
    loadCategoriesList();
});

function loadCategoriesList() {
    dataTable = $("#DT_Load").DataTable({
        "ajax": {
            "url": "/api/foodtype",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                        <a href="/admin/foodtype/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                            <i class="fas fa-edit">edit</i>
                        </a>
                        <a href="#" class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=DeleteCategory('/api/foodtype/'+${data})>
                            <i class="fas fa-trash-alt">delete</i>
                        </a>
                    </div> `;
                },
                "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}


function DeleteCategory(url) {
    console.log(url);
    swal({
        title: "Delete Food Type",
        text: "Are you sure you need to delete this Food Type?",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }

            });
        }
    });
}
