let baseUrlDelete = window.location.href.split("Index")[0];
let selectedDeleteId = null;
$(".delete").click(function () {
    selectedDeleteId = $(this).data("id");
});

$("#confirmDelete").click(function () {
    if (selectedDeleteId) {
        window.location.href = `${baseUrlDelete}Delete?id=${selectedDeleteId}&pageNumber=${currentPage}`;
    }
});