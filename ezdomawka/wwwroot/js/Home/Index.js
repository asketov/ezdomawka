$(function () {
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 500,
        values: [75, 300],
        slide: function (event, ui) {
            $("#amount").val("Р" + ui.values[0] + " - Р" + ui.values[1]);
        }
    });
    $("#amount").val("Р" + $("#slider-range").slider("values", 0) +
        " - Р" + $("#slider-range").slider("values", 1));
});