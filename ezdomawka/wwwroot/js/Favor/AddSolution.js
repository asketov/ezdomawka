let selectedSubjects = [];
$(document).ready(function () {
    var choicesSelect = new Choices('#subjects', {
        allowHTML: true,
        removeItemButton: true,
    });

    choicesSelect.passedElement.element.addEventListener(
        'addItem',
        function(event) {
            selectedSubjects.push({ id: event.detail.value, name: event.detail.label });
        }
    );

    choicesSelect.passedElement.element.addEventListener(
        'removeItem',
        function(event) {
            selectedSubjects.filter(el => el.id != event.detail.value);
        }
    );
    
    $("#addFavor").click(function () {
        if ($("#form").valid())
        {
            $.ajax({
                url: '/FavorSolution/AddSolution',
                method: 'post',
                dataType: 'json',
                data: {
                    Subjects: selectedSubjects,
                    Theme: {id: $("#themes option").val(), name: $("#themes option").html()}, Text: $("#text").val(),
                    Price: $('#price').val(), Connection: $('#connect').val()
                },
                success: function (data) {
                    if (data.redirect) {
                        window.location = '/home/index'
                    } else {
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
