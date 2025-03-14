const prevPageBtn = document.getElementById('prevPage');
const nextPageBtn = document.getElementById('nextPage');
let lastPage = parseInt(nextPageBtn.getAttribute('data-page')) || 0;
let currentPage = getQueryParameter('pageNumber');

function getQueryParameter(name) {
    const urlParams = new URLSearchParams(window.location.search);
    return parseInt(urlParams.get(name)) || 0;
}

function updatePageNumber(pageNumber) {
    const url = new URL(window.location.href);
    url.searchParams.set('pageNumber', pageNumber);
    return url.toString();
}

function updatePaginationButtons() {
    if (currentPage <= 0) {
        prevPageBtn.classList.add("disabled");
    } else {
        prevPageBtn.classList.remove("disabled");
    }

    if (currentPage >= lastPage) {
        nextPageBtn.classList.add("disabled");
    } else {
        nextPageBtn.classList.remove("disabled");
    }
}

prevPageBtn.addEventListener('click', function () {
    if (currentPage > 0) {
        currentPage--;
        window.location.href = updatePageNumber(currentPage);
    }
});

nextPageBtn.addEventListener('click', function () {
    if (currentPage < lastPage) {
        currentPage++;
        window.location.href = updatePageNumber(currentPage);
    }
});

updatePaginationButtons();