$(document).ready(function () {
    let ThemeId = null;
    let SubjectId = null;

    $("#submitForm").click(function () {
        let selectedThemeId = $("#themes option:selected").val();
        let selectedSubjectId = $("#subjects option:selected").val();
        if (selectedSubjectId && selectedThemeId) {
            $.ajax({
                url: '/FavorSolution/FindFavors/',
                method: 'get',
                dataType: 'html',
                data: {
                    ThemeId: selectedThemeId, SubjectId: selectedSubjectId
                },
                success: function (data) {
                    $('#FavorsWithPagination').empty();
                    $('#FavorsWithPagination').append(data);
                    ThemeId = selectedThemeId;
                    SubjectId = selectedSubjectId;
                    window.scrollTo(0, 0);
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

    $(document).on('click', '.page', function (event) {
        let skip = (Number.parseInt(event.currentTarget.value) - 1) * 10;
        $.ajax({
            url: '/FavorSolution/GetSolutions/',
            method: 'get',
            dataType: 'html',
            data: {
                ThemeId: ThemeId, SubjectId: SubjectId, Take: 10, Skip: skip
            },
            success: function (data) {
                $('#favorSolutions').replaceWith(data);
                window.scrollTo(0, 0);
            },
            statusCode: {
                400: function () { 
                    alert("Неправильный запрос");
                },
                404: function () { 
                    alert("Страница не найдена");
                }
            }

        });
    })

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
    console.log(countPages);

});

