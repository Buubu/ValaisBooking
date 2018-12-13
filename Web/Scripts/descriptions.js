
// Script permettant de séparer les descriptions
var descriptions = document.getElementsByName("descriptionHotel");
var descriptionsParts = document.getElementsByName("descriptionPart");

for (var i = 0; i < descriptions.length; i++) {
    // Je commence par récupérer le contenu de chaque description
    var content = descriptions[i].textContent;

    // Je sépare ensuite les descriptions en deux pour ne pas qu'elles soient trop longues au premier regard (2 paragraphes)
    var first = content.indexOf(">");
    var second = content.indexOf(">", first + 1);
    var third = content.indexOf(">", second + 1);
    var forth = content.indexOf(">", third + 1);

    var firstPart = content.substr(0, forth);
    var secondPart = content.substr(forth + 1);

    // Je réaffecte ensuite les contenus dans les différentes parties
    descriptions[i].innerHTML = firstPart;
    descriptionsParts[i].innerHTML = secondPart;
}