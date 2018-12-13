
// Script permettant de changer le numéro de la catégorie en étoiles
var categories = document.getElementsByName("categoryHotel");

for (var i = 0; i < categories.length; i++) {
    var number = categories[i].textContent;
    var stars = "";
    var counter = 0;

    while (counter < number) {
        stars += "★";
        counter++;
    }

    categories[i].innerHTML = stars;
}