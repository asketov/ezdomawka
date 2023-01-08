


$(document).ready(function () {
    let selectedSubjects = [];

    $("#addSubject").click(function () {
        if ($("#subjects option").length > 0) {
            selectedSubjects.push({ id: $("#subjects option:selected").val(), name: $("#subjects option:selected").html() });
            console.log(selectedSubjects);
            $(".SelectedSubjects")
                .append(`<div class='${$("#subjects option:selected").val()} d-flex justify-content-center pt-2 box'><div class='selectedSubject p-3'>
                    <i class='item'>${$("#subjects option:selected").html()}</i>
                    <button value='${$("#subjects option:selected").val()}' id='deleteSubjectButton' 
                    type='button' class='item btn btn-dark'><i class='fa-solid fa-trash'></i>
                    </button></div></div>`);
            $("#subjects :selected").remove();
            
        }
    });

    $(document).on('click', '#deleteSubjectButton', function (event) {
        let id = event.currentTarget.value;
        optionText = selectedSubjects.find(el => el.id == id).name;
        optionValue = id;
        $('#subjects').append(`<option value="${optionValue}">${optionText}</option>`);
        selectedSubjects = selectedSubjects.filter(el => el.id != id);
        console.log(selectedSubjects);
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
