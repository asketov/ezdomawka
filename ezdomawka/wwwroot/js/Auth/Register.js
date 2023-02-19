$(document).ready(function () {
    
    $('#continue').click(function () {
        if ($("#form").valid()) {
            $.ajax({
                url: '/Auth/ConfirmEmail/',
                method: 'post',
                dataType: 'html',
                data: {
                    Email: $("#email").val(), Nick: $("#nick").val(), Password: $("#password").val(),
                    ConfirmPassword: $("#confirmPassword").val()
                },
                success: function () {
                    $("#email").prop('disabled', true);
                    $("#nick").prop('disabled', true);
                    $("#password").prop('disabled', true);
                    $("#confirmPassword").prop('disabled', true);
                    $(".code").attr("hidden", false);
                    $('#buttons').empty();
                    $('#buttons').append("<button id='send' type='button' class='btn btn-dark'>Зарегистрироваться</button >");
                },
                error: function (data) {
                    if (data.modelState) {
                        $.each(data.modelState, function (i, item) {
                            item.valid();
                        });
                    }
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
    
    $(document).on('click', '#send', function () {
        $.ajax({
            url: '/Auth/Register/',
            method: 'post',
            dataType: 'html',
            data: {
                Email: $("#email").val(), Nick: $("#nick").val(), Password: $("#password").val(),
                ConfirmPassword: $("#confirmPassword").val(), ConfirmCode: $('#code').val()
            },
            success: function () {
                window.location = '/home/index';
            },
            error: function (data) {
                if (data.modelState) {
                    $.each(data.modelState, function (i, item) {
                        item.valid();
                    });
                }
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


})