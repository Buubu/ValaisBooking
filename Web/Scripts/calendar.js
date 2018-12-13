
//Script trouvé sur internet, sur https://eonasdan.github.io/bootstrap-datetimepicker/
$(function () {
    $("#checkIn").datetimepicker({
        format: "DD MMMM YYYY"
    });
    $("#checkOut").datetimepicker({
        useCurrent: false,
        format: "DD MMMM YYYY"
    });
    $("#checkIn").on("dp.change", function (e) {
        $('#checkOut').data("DateTimePicker").minDate(e.date);
    });
    $("#checkOut").on("dp.change", function (e) {
        $('#checkIn').data("DateTimePicker").maxDate(e.date);
    });
});