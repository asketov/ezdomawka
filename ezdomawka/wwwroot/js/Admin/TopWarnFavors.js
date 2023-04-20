
$(document).ready(function () {

    $(document).on('click', '.page', function (event) {
        let numberPage = Number.parseInt(event.currentTarget.value);
        let skip = (numberPage - 1) * 10;
        $('#favorSolutions').append("<div class='pt-4'><div class='loader'></div></div>");
        $.ajax({
            url: '/Admin/TopSolutionsByReports/',
            method: 'get',
            dataType: 'html',
            data: {
                Take: 10, Skip: skip
            },
            success: function (data) {
                $('#favorSolutions').empty();
                $('#favorSolutions').replaceWith(data);
                window.scrollTo(0, 0);
                PaintCurrentPage("#6A8FD9");
                currentPage = numberPage;
                PaintCurrentPage("#297eff");
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
    });

    $(document).on('click', '#deleteWarns', function (event) {
            let favorId = event.currentTarget.value;
            $('.Content').css('min-height', '200px');
            $('.Content').empty();
            $('.Content').append("<div><div class='loader'></div></div>");
            $.ajax({
                url: '/Admin/DeleteWarns/',
                method: 'post',
                dataType: 'json',
                data: {
                    FavorId: favorId
                },
                success: function () {
                    $('.Content').empty();
                    $('.Content').css('min-height', '10px');
                    $('.Content').append(
                        `<div class='text-center' style='width: 300px;'><div class='pb-2'>
                            Жалобы успешно очищены <i class="fa-regular fa-circle-check"></i></div>
                            <button id='CloseModal' class='btn btn-outline-secondary'>Закрыть</button></div>`);
                    location.reload();
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
    });

});