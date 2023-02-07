

let selectedSubjects = [];
$(document).ready(function () {
    $("#addSubject").click(function () {
        let selectedOption = $("#subjects :selected");
        if (selectedOption.val() == 'select_all') {
            selectedSubjects = [];
            $("#subjects option").each(function () { selectedSubjects.push({ id: $(this).val(), name: $(this).html() }) });
            selectedSubjects = selectedSubjects.filter(el => el.id != "select_all");
            $(".SelectedSubjects").empty();
        }
        else if (!selectedSubjects.some(x => x.id == selectedOption.val())) {
            selectedSubjects.push({ id: selectedOption.val(), name: selectedOption.html() });
        }
        else return;
        $(".SelectedSubjects")
            .append(`<div class='${selectedOption.val()} d-flex justify-content-center pt-2 box'><div class='selectedSubject p-3'>
                <i class='item'>${selectedOption.html()}</i>
                <button value='${selectedOption.val()}' id='deleteSubjectButton' 
                type='button' class='item btn btn-dark'><i class='fa-solid fa-trash'></i>
                </button></div></div>`);
    });

    $(document).on('click', '#deleteSubjectButton', function (event) {
        let id = event.currentTarget.value;
        if (id == "select_all") {
            selectedSubjects = [];
        }
        else {
            selectedSubjects = selectedSubjects.filter(el => el.id != id);
        }
        $(`.${id}`).remove();
    });


    $("#submitForm").click(function () {
        if ($("#form").valid() && selectedSubjects.length > 0) { 
            $.ajax({
                url: '/FavorSolution/AddSolution',
                method: 'post',
                dataType: 'json',
                data: {
                    Subjects: selectedSubjects, 
                    Theme: { id: $("#themes option:selected").val(), name: $("#themes option:selected").html() }, Text: $("#text").val(),
                    Price: $('#price').val(),  Connection: $('#connect').val() },
                success: function (data) {
                    if(data.redirect) {
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
