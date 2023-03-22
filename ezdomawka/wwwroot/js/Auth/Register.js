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
                    document.getElementById('<%=Email%>').prop('disabled', true);
                    document.getElementById('<%=Nick%>').prop('disabled', true);
                    document.getElementById('<%=Password%>').prop('disabled', true);
                    document.getElementById('<%=ConfirmPassword%>').prop('disabled', true);
                    document.getElementById('<%=ConfirmCode%>').attr("hidden", false);
                    $('#buttons').empty();
                    $('#buttons').append("<fieldset class=\"login_button\">\n" +
                        "                      <button type=\"submit\" id=\"form-submit\" class=\"orange-button\">Создать</button>\n" +
                        "                 </fieldset>");
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