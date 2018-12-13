
// Récupération de la valeur du "count" et réaffectation à l'élément dont l'id est "category"
function change() {
    var total = document.getElementById("count").textContent;
    document.getElementById("category").value = total;
}    