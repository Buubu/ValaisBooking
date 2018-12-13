
// Script permettant de séparer les descriptions
var descriptions = document.getElementsByName("descriptionHotel");
var descriptionsParts = document.getElementsByName("descriptionPart");

for (var i = 0; i < descriptions.length; i++) {
    // Je commence par récupérer le contenu de chaque description
    var content = descriptions[i].textContent;

    // Je sépare ensuite les descriptions en deux pour ne pas qu'elles soient trop longues au premier regard (4 paragraphes affichés)
    var index = content.indexOf(">");
    var first = content.indexOf(">", index + 1);
    var index2 = content.indexOf(">", first + 1);
    var second = content.indexOf(">", index2 + 1);
    var index3 = content.indexOf(">", second + 1);
    var third = content.indexOf(">", index3 + 1);
    var index4 = content.indexOf(">", third + 1);
    var fourth = content.indexOf(">", index4 + 1);

    var firstPart = content.substr(0, fourth);
    var secondPart = content.substr(fourth + 1);

    // Je réaffecte ensuite les contenus dans les différentes parties
    descriptions[i].innerHTML = firstPart;
    descriptionsParts[i].innerHTML = secondPart;
}