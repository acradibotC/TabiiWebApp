﻿let table = new DataTable('#DT_load', {
    "ajax": {
        "url": "/api/MenuItem",
        "type": "GET",
        "datatype": "json"
    },
    "columns": [
        {"data":"name","width":"25%"},
        { "data": "price", "width": "15%" },
        { "data": "category.name", "width": "15%" },
        { "data": "foodType.name", "width": "15%" },
        {
            "data": "id",
            "render": function (data) {
                return `<div class="w-75 btn-group">
                            <a href="/Admin/MenuItems/upsert?id=${data}" class="btn btn-primary text-white mx-2">
                            <i class="bi bi-pencil-square"></i></a>
                            <a onClick=Delete('/api/MenuItem/'+${data}) class="btn btn-danger text-white mx-2">
                            <i class="bi bi-trash3-fill"></i>
                        </div >`
            },
            "width": "15%"
        },
    ],
    "width":"100%"

});

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire(
                $.ajax({
                    url: url,
                    type: 'delete',
                    success: function (data) {
                        if (data.success) {
                            table.ajax.reload();
                            //success notification
                            toastr.success(data.message);
                        }
                        else {
                            //failure notification
                            toastr.error(data.message);

                        }
                    }
                })
            )
        }
    })
}