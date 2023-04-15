
$(document).ready(function () {
    $("#findUsers").click(function () {
        let blockUsersPagination = $('#UsersWithPagination');
        blockUsersPagination.empty();
        blockUsersPagination.append("<div class='loader'></div>");
        let email = $("#InputEmail").val(), nick = $("#InputNick").val();
        $.ajax({
            url: '/Admin/UsersWithPagination',
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
        let numberPage = Number.parseInt(event.currentTarget.value);
        let skip = (numberPage - 1) * 10;
        let email = $("#InputEmail").val(), nick = $("#InputNick").val();
        $('#Users').append("<div class='pt-4'><div class='loader'></div></div>");
        $.ajax({
            url: '/Admin/UserTable/',
            method: 'get',
            dataType: 'html',
            data: {
                Take: 10, Skip: skip, Email: email, Nick: nick
            },
            success: function (data) {
                $('#Users').empty();
                $('#Users').replaceWith(data);
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
    $("#sortOptions").on('change', function () {
        let blockUsersPagination = $('#UsersWithPagination');
        blockUsersPagination.empty();
        blockUsersPagination.append("<div class='loader'></div>");
        let email = $("#InputEmail").val(), nick = $("#InputNick").val();
        $.ajax({
            url: '/Admin/UsersWithPagination',
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
    $(document).on('click', '#BanUser', function (event) {
        let userId = event.currentTarget.value;
        $('.Content').css('min-height', '10px');
        $('.Content').append(`<form id='form'><div class='text-center'>
                <textarea class='form-control' placeholder='Причина бана' 
                         rows='3' cols='50' required maxlength='200' id='reasonBan'></textarea>
                <input class='form-control mt-2' id='durationBan' value='0' required placeholder='Длительность' type='number'></input>
                <div class='text-center pt-2'><button value='${userId}' id='addBan' class='btn btn-outline-dark'>Забанить</button>
                <button id='CloseModal' class='btn btn-outline-secondary'>Отмена</button></div></form>`);
        $('.Modal').addClass('Active');
    });
    $(document).on('click', '.Modal', function (event) {
        $('.Content').css('min-height', '200px');
        event.stopPropagation();
        $('.Modal').removeClass('Active');
        $('.Content').empty();
    });

    $(document).on('click', '#CloseModal', function (event) {
        $('.Content').css('min-height', '200px');
        event.stopPropagation();
        $('.Modal').removeClass('Active');
        $('.Content').empty();
    });

    $(document).on('click', '.Content', function (event) {
        event.stopPropagation();
    });
    $(document).on('click', '#addBan', function (event) {
        event.preventDefault();
        let reasonBan = $('#reasonBan').val();
        let durationBan = $('#durationBan').val();
        if ($("#form").valid() && reasonBan != '' && durationBan != '') {
            let userId = event.currentTarget.value;
            $('.Content').css('min-height', '200px');
            $('.Content').empty();
            $('.Content').append("<div><div class='loader'></div></div>");
            $.ajax({
                url: '/Admin/BanUser/',
                method: 'post',
                dataType: 'json',
                data: {
                    UserId: userId, Reason: reasonBan, Duration: durationBan
                },
                success: function () {
                    $('.Content').empty();
                    $('.Content').css('min-height', '10px');
                    $('.Content').append(
                        `<div class='text-center' style='width: 300px;'><div class='pb-2'>
                            Пользователь забанен <i class="fa-regular fa-circle-check"></i></div>
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
        }
    });
    $(document).on('click', '#unBan', function (event) {
        event.preventDefault();
        let userId = event.currentTarget.value;
        $('.Content').css('min-height', '200px');
        $('.Content').empty();
        $('.Content').append("<div><div class='loader'></div></div>");
        $('.Modal').addClass('Active');
        $.ajax({
            url: '/Admin/UnBanUser/',
            method: 'post',
            dataType: 'json',
            data: {
                userId: userId
            },
            success: function () {
                $('.Content').empty();
                $('.Content').css('min-height', '10px');
                $('.Content').append(
                    `<div class='text-center' style='width: 300px;'><div class='pb-2'>
                        Пользователь разбанен <i class="fa-regular fa-circle-check"></i></div>
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