


$(document).ready(function () {
    let selectedSubjects = [];
    $("#addSubject").click(function () {
        let options = $("#subjects option"), selectedOption = $("#subjects :selected");
        if (options.length > 0)
        {
            if (selectedOption.val() == 'select_all') {
                $('#subjects option:first').remove();
                $("#subjects option").each(function () { selectedSubjects.push({ id: $(this).val(), name: $(this).html() }) });
                $("#subjects").empty();
                $(".SelectedSubjects").empty();
            }
            else {
                selectedSubjects.push({ id: selectedOption.val(), name: selectedOption.html() });
                selectedOption.remove();
            }
            $(".SelectedSubjects")
                .append(`<div class='${selectedOption.val()} d-flex justify-content-center pt-2 box'><div class='selectedSubject p-3'>
                    <i class='item'>${selectedOption.html()}</i>
                    <button value='${selectedOption.val()}' id='deleteSubjectButton' 
                    type='button' class='item btn btn-dark'><i class='fa-solid fa-trash'></i>
                    </button></div></div>`);
        }
    });

    $(document).on('click', '#deleteSubjectButton', function (event) {
        let id = event.currentTarget.value;
        if (id == "select_all") {
            $('#subjects').prepend(`<option value="select_all">Выбрать все предметы</option>`);
            selectedSubjects.forEach(x =>
                $('#subjects').append(`<option value="${x.id}">${x.name}</option>`));
            selectedSubjects = [];
        }
        else {
            optionText = selectedSubjects.find(el => el.id == id).name;
            optionValue = id;
            $('#subjects').append(`<option value="${optionValue}">${optionText}</option>`);
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
