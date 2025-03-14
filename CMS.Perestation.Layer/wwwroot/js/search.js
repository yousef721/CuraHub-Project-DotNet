$(document).ready(function () {
    const searchInput = $("#searchInput");
    let searchTimeout;
    let baseUrlSearch = window.location.href.split("Index")[0];
    let title = document.querySelector(".title").textContent;
    var table = document.getElementById('table');
    var numberOfColumns = table.rows[0].cells.length;

    function fetchSearchResults() {
        const searchText = searchInput.val().trim();
        let searchFilters = {};
    
        $(".select-filter").each(function () {
            let key = $(this).attr("id");
            let value = $(this).val();
            if (value.length > 0) {
                searchFilters[key] = value;
            }
        });
    
        if (searchText.length > 0 || Object.keys(searchFilters).length > 0) {
            $.ajax({
                url: `${baseUrlSearch}Search`,
                type: "GET",
                data: { searchText: searchText, ...searchFilters },
                success: function (response) {
                    if (response.trim() === "") {
                        $("#Content tbody").html(`<tr><td colspan='${numberOfColumns}' class='text-center text-danger'>Not ${title} Found.</td></tr>`);
                    } else {
                        console.log(response);
                        $("#Content tbody").html(response);
                    }
                }

            });
        } else {
            location.reload();
        }
    }
    
    searchInput.on("input", function () {
        clearTimeout(searchTimeout);
        searchTimeout = setTimeout(fetchSearchResults, 300);
    });
    
    $(".select-filter").on("change", function () {
        fetchSearchResults();
    });
});
