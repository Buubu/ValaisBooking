
// Débloque le bouton de réservation lorsqu'au moins une checkbox est sélectionnée
var checkBoxes = $('.myCheck');
        checkBoxes.change(function () {
            $('button[type=submit]').prop('disabled', checkBoxes.filter(':checked').length < 1);
        });
checkBoxes.change();