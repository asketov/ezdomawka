$(document).ready(function () {
    pages = [];

    $(function () {
        $("#slider-range").slider({
            range: true,
            min: 0,
            max: 500,
            values: [75, 300],
            slide: function (ui) {
                $("#amount").val("Р" + ui.values[0] + " - Р" + ui.values[1]);
            }
        });
        $("#amount").val("Р" + $("#slider-range").slider("values", 0) +
            " - Р" + $("#slider-range").slider("values", 1));
    });

    $("#submitForm").click(function () {
        if ($("#form").valid() && selectedSubjects.length > 0) {
            $.ajax({
                url: '/FavorSolution/AddSolution',
                method: 'get',
                dataType: 'html',
                data: {
                    Subjects: selectedSubjects,
                    Theme: { id: $("#themes option:selected").val(), name: $("#themes option:selected").html() }, Text: $("#text").val(),
                    Price: $('#price').val(), Connection: $('#connect').val()
                },
                success: function (data) {
                    if (data.redirect) {
                        window.location = '/home/index'
                    }
                    else {
                        window.location = data.redirect
                    }
                },
                statusCode: {
                    400: function () { // выполнить функцию если код ответа HTTP 400
                        alert("Неправильный запрос");
                    },
                    404: function () { // выполнить функцию если код ответа HTTP 404
                        alert("Страница не найдена");
                    }
                }

            });
        }
    });


});

