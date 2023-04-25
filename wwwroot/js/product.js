var dataTable;

$(document).ready(function () {
  loadDataTable();
});

function loadDataTable() {
  dataTable = $("#tblData").DataTable({
    ajax: { url: "/admin/products/getall" },
    columns: [
      { data: "title", width: "25%" },
      { data: "isbn", width: "15%" },
      { data: "listPrice", width: "10%" },
      { data: "author", width: "15%" },
      { data: "category.name", width: "10%" },
      {
        data: "id",
        render: function (data) {
          return `<div class="w-75 btn-group text-center" role=group >
            
            <a href="/admin/products/upsert?id=${data}" class= "btn btn-dark mx-2> <i class="bi bi-pencil-square"></i> Edit</a>
            <a onClick=Delete("/admin/products/delete/${data}")  class= "btn btn-danger mx-2> <i class="bi bi-trash3-fill"></i> Delete</a>

            </div>`;
        },
        width: "25%",
      },
    ],
  });
}

function Delete(url) {
  Swal.fire({
    title: "Bạn có muốn xóa ?",
    text: "Lưu ý nội dung xóa không thể khôi phục !",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Delete !",
  }).then((result) => {
    if (result.isConfirmed) {
      $.ajax({
        url: url,
        type: "DELETE",
        success: function (data) {
          dataTable.ajax.reload();
          toastr.success(data.message);
        },
      });
    }
  });
}
