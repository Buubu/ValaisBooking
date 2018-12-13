
// Script qui calcule la durée entre deux dates et l'affiche dans la bonne variable
function duration() {
    var start = moment(document.getElementById("checkIn").value);
    var end = moment(document.getElementById("checkOut").value);

    var duration = end.diff(start, "days");

    if (isNaN(duration)) {
        duration = 0;
    }

    // Le bouton de recherche est bloqué tant que le nombre de nuits n'est pas supérieur à 0
    $('button[type=submit]').prop('disabled', duration < 1);

    document.getElementById("nights").innerHTML = duration;
}