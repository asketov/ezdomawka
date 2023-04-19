
$(document).ready(function () {
    $(document).on('click', '.page', function (event) {
        let numberPage = Number.parseInt(event.currentTarget.value);
        let skip = (numberPage - 1) * 10;
        $('#bans').append("<div class='pt-4'><div class='loader'></div></div>");
        $.ajax({
            url: '/Admin/UserBans/',
            method: 'get',
            dataType: 'html',
            data: {
                Take: 10, Skip: skip, UserId: userId
            },
            success: function (data) {
                $('#bans').empty();
                $('#bans').replaceWith(data);
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

});