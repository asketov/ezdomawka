


$(document).ready(function () {
    $("#TOTP_lb")
    let selectedThemes = [];
    let selectedSubjects = [];

    $("#addTheme").click(function () {
        if ($("#themes option").length > 0) {
            selectedThemes.push({ id: $("#themes option:selected").val(), name: $("#themes option:selected").html() });
            console.log(selectedThemes);
            $(".SelectedThemes")
                .append(`<div class='d-flex justify-content-center box pt-2 ${$("#themes option:selected").val()}'>
                        <div class='selectedTheme p-3'>
                        <i class='item'>${$("#themes option:selected").html()}</i>
                        <button value='${$("#themes option:selected").val()}' id='deleteThemeButton' type='button' 
                        class='item btn btn-dark'><i class='fa-solid fa-trash'></i></button></div></div>`);
            $("#themes :selected").remove();
        }
    });

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

    $(document).on('click', '#deleteThemeButton', function (event) {
        let id = event.currentTarget.value;
        optionText = selectedThemes.find(el => el.id == id).name;
        optionValue = id;
        $('#themes').append(`<option value="${optionValue}">${optionText}</option>`);
        selectedThemes = selectedThemes.filter(el => el.id != id);
        console.log(selectedThemes);
        $(`.${id}`).remove();
    });

    $("#submit").click(function () {
        if (selectedSubjects.length > 0 && selectedThemes.length > 0) { 
            $.ajax({
                url: '/FavorSolution/AddSolution',
                method: 'post',
                dataType: 'html',
                data: {
                    Subjects: selectedSubjects, Themes: selectedThemes, Text: $("#text").val(),
                    Price: $('#price').val(),  Connection: $('#connect').val() },
                success: function (data) {
                    if (data.redirect) {
                        // data.redirect contains the string URL to redirect to
                        window.location.href = data.redirect
                    }
                }
            });
        }
    });
});
