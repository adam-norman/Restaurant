
var dataTable;
$(document).ready(function () {
    loadCategoriesList();
});

function loadCategoriesList() {
    dataTable = $("#DT_Load").DataTable({
        "ajax": {
            "url": "/api/user",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fullName", "width": "25%" },
            { "data": "email", "width": "25%" },
            { "data": "phoneNumber", "width": "20%" },
            {
                "data": {id:  'id', lockoutEnd: 'lockoutEnd'},
                "render": function (data) {
                    var todayDate = new Date().getTime();
                    var lockendDate = new Date(data.lockoutEnd).getTime();
                    // already locked
                    if (todayDate < lockendDate) {
                        console.log('unlock');
                        return `
                    <div class="text-center">
                        <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=UserAccountLockToggler('${data.id}','${'UnLock'}')>
                         <i class="fas fa-lock-open"></i> Unlock
                        </a>
                    </div>`;
                    } else {
                        return `
                    <div class="text-center">
                        <a class="btn btn-success text-white" style="cursor:pointer; width:100px;" onclick=UserAccountLockToggler('${data.id}','${'Lock'}')>
                              <i class="fas fa-lock"></i> Lock
                        </a>
                    </div>`;
                    }
                     
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


function UserAccountLockToggler(id,toggleAction) {
    
    swal({
        title:  toggleAction+" the user account",
        text: "Are you sure you need to "+toggleAction+"?",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((toggle) => {
        if (toggle) {
            $.ajax({
                url: '/api/user',
                type: 'POST',
                data: JSON.stringify(id),
                contentType: 'application/json; charset=UTF-8',
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
