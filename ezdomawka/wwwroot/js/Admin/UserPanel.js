
$(document).ready(function () {
    $("#findUsers").click(function () {
        let blockUsersPagination = $('#UsersWithPagination');
        blockUsersPagination.empty();
        blockUsersPagination.append("<div class='loader'></div>");
        let email = $("#InputEmail").val(), nick = $("#InputNick").val();
        $.ajax({
            url: '/Admin/GetUsersWithPagination',
            method: 'get',
            dataType: 'html',
            data: {
               Email: email, Nick: nick
            },
            success: function (data) {
                blockUsersPagination.empty();
                blockUsersPagination.append(data);
                window.scrollTo(0, 0);
            },
            statusCode: {
                400: function () { // выполнить функцию если код ответа HTTP 400
                    blockUsersPagination.empty();
                    blockUsersPagination.append(`<div class='d-flex justify-content-center pt-3'>
                                                <div class='text-danger'>Запрос введён неверно</div></div>`);
                },
                404: function () { // выполнить функцию если код ответа HTTP 404
                    alert("Страница не найдена");
                }
            }

        });
    });

    $(document).on('click', '.page', function (event) {
        let skip = (Number.parseInt(event.currentTarget.value) - 1) * 10;
        let email = $("#InputEmail").val(), nick = $("#InputNick").val();
        $('#Users').append("<div class='pt-4'><div class='loader'></div></div>");
        $.ajax({
            url: '/Admin/GetUserTable/',
            method: 'get',
            dataType: 'html',
            data: {
                Take: 10, Skip: skip, Email: email, Nick: nick
            },
            success: function (data) {
                $('#Users').empty();
                $('#Users').replaceWith(data);
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
    });
    $("#sortOptions").on('change', function () {
        let blockUsersPagination = $('#UsersWithPagination');
        blockUsersPagination.empty();
        blockUsersPagination.append("<div class='loader'></div>");
        let email = $("#InputEmail").val(), nick = $("#InputNick").val();
        $.ajax({
            url: '/Admin/GetUsersWithPagination',
            method: 'get',
            dataType: 'html',
            data: {
                Email: email, Nick: nick
            },
            success: function (data) {
                blockUsersPagination.empty();
                blockUsersPagination.append(data);
                window.scrollTo(0, 0);
            },
            statusCode: {
                400: function () { // выполнить функцию если код ответа HTTP 400
                    blockUsersPagination.empty();
                    blockUsersPagination.append(`<div class='d-flex justify-content-center pt-3'>
                                                <div class='text-danger'>Запрос введён неверно</div></div>`);
                },
                404: function () { // выполнить функцию если код ответа HTTP 404
                    alert("Страница не найдена");
                }
            }
        });
    });
});