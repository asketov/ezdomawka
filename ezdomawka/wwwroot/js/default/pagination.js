$(document).ready(function () {
    let currentPage = 1;
    let currentListPages = 1;
    let isLastListPages = false;
    let restCountPages = countPages % 5;
    let fullListPages = Math.trunc(countPages / 5);
    if (restCountPages > 0) fullListPages++;
    $(document).on('click', '#previousPages', function (event) {
        if (currentListPages > 1) {
            if (isLastListPages) {
                isLastListPages = !isLastListPages;
                let firstNumPage = (currentListPages - 2) * 5 + 1;
                $("#pages").empty();
                for (let i = 0; i < 5; i++) {
                    $('#pages').append(`<button style="width: 30px;" class="nav_menu_button page" value="${firstNumPage + i}">${firstNumPage + i}</button>`);
                }
                $('#nextPages').css('background-color', "#6A8FD9");
            }
            else {
                $(".page").each(function () {
                    let num = Number.parseInt($(this).text());
                    $(this).text(num - 5);
                    $(this).val(num - 5);
                });
            }
            currentListPages--;
            if (currentListPages == 1) $('#previousPages').css('background-color', "gray");
        }
    });

    $(document).on('click', '#nextPages', function (event) {
        if (!isLastListPages) {
            if (currentListPages + 1 == fullListPages) {
                let firstNumPage = currentListPages * 5 + 1;
                $("#pages").empty();
                isLastListPages = !isLastListPages;
                for (let i = 0; i < restCountPages; i++) {
                    $('#pages').append(`<button style="width: 50px;" class="nav_menu_button page" value="${firstNumPage + i}">${firstNumPage + i}</button>`);
                }
                $('#nextPages').css('background-color', "gray");
            }
            else {
                $(".page").each(function () {
                    let num = Number.parseInt($(this).text());
                    $(this).text(num + 5);
                    $(this).val(num + 5);
                });
            }
            currentListPages++;
            if (currentListPages == 2) $('#previousPages').css('background-color', "#6A8FD9");
        }
    });
});