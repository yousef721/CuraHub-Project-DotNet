// $(document).ready(function () {
//     const searchInput = $(".search-input");
//     const searchResults = $(".search-results");
//     const cartCountElement = $(".cart-count");

//     function fetchSearchResults(searchUrl) {
//         const searchText = searchInput.val().trim();
//         if (searchText.length > 0) {
//             $.ajax({
//                 url: searchUrl,
//                 type: "GET",
//                 data: { searchText: searchText },
//                 success: function (response) {
//                     searchResults.html(response).show();
//                 }
//             });
//         } else {
//             location.reload();
//         }
//     }

//     searchInput.on("input", function () {
//         const searchUrl = $(this).data("search-url");
//         fetchSearchResults(searchUrl);
//     });

//     function getCartCount(cartCountUrl) {
//         $.ajax({
//             url: cartCountUrl,
//             type: "GET",
//             success: function (response) {
//                 if (response) {
//                     cartCountElement.text(response.count);
//                 }
//             }
//         });
//     }

//     function updateCart(medicineId, action, cartUrl, event) {
//         event.preventDefault();
//         if (!window.isAuthenticated) {
//             window.location.href = "/Identity/Account/Login";
//             return;
//         }

//         $.ajax({
//             url: `${cartUrl}/${action}`,
//             type: "POST",
//             data: { medicineId: medicineId },
//             success: function (response) {
//                 if (response) {
//                     let countElement = $("#count-" + medicineId);
//                     let deleteButton = $("#delete-" + medicineId);
//                     let addButton = $("#adder-" + medicineId);
//                     let plusButton = $("#plus-" + medicineId);
//                     let minusButton = $("#minus-" + medicineId);
//                     let cartControls = $("#cart-" + medicineId);

//                     countElement.text(response.count);

//                     if (response.count > 0) {
//                         addButton.hide();
//                         cartControls.show();
//                         plusButton.show();
//                         countElement.show();

//                         if (response.count === 1) {
//                             minusButton.hide();
//                             deleteButton.show();
//                         } else {
//                             minusButton.show();
//                             deleteButton.hide();
//                         }
//                     } else {
//                         addButton.show();
//                         cartControls.hide();
//                     }

//                     getCartCount(cartUrl + "/GetCartCount");
//                 }
//             }
//         });
//     }

//     $(".update-cart-btn").on("click", function (event) {
//         const medicineId = $(this).data("medicine-id");
//         const action = $(this).data("action");
//         const cartUrl = $(this).data("cart-url");
//         updateCart(medicineId, action, cartUrl, event);
//     });

//     $(".delete-item-btn").on("click", function (event) {
//         event.preventDefault();
//         const medicineId = $(this).data("medicine-id");
//         const cartUrl = $(this).data("cart-url");

//         $.ajax({
//             url: `${cartUrl}/DeleteFromCart`,
//             type: "POST",
//             data: { medicineId: medicineId },
//             success: function () {
//                 $(`#adder-${medicineId}`).show();
//                 $(`#cart-${medicineId}`).hide();
//                 $(`#count-${medicineId}`).hide();
//                 getCartCount(cartUrl + "/GetCartCount");
//             }
//         });
//     });

//     // Auto-load cart count
//     $(".cart-container").each(function () {
//         getCartCount($(this).data("cart-url") + "/GetCartCount");
//     });
// });
