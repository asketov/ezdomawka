$(document).ready(function () {
    let currentListPages = 1;
    let isLastListPages = false;
    let restCountPages = countPages % 5;
    let fullListPages = Math.trunc(countPages / 5);
    if (restCountPages > 0) fullListPages++;
    $(document).on('click', '.previousPages', function (event) {
        if (currentListPages > 1) {
            PaintCurrentPage("#6A8FD9");
            if (isLastListPages) {
                isLastListPages = !isLastListPages;
                let firstNumPage = (currentListPages - 2) * 5 + 1;
                $(".pages").empty();
                for (let i = 0; i < 5; i++) {
                    $('.pages').append(`<button class="nav_menu_button page" value="${firstNumPage + i}">${firstNumPage + i}</button>`);
                }
                $('.nextPages').css('background-color', "#6A8FD9");
            }
            else {
                $(".page").each(function () {
                    let num = Number.parseInt($(this).text());
                    $(this).text(num - 5);
                    $(this).val(num - 5);
                });
            }
            currentPage = (currentListPages - 2) * 5 + 1;
            PaintCurrentPage("#6A8FD9");
            ClickCurrentPage();
            currentListPages--;
            if (currentListPages == 1) $('.previousPages').css('background-color', "#bdbbbb");
        }
    });

    $(document).on('click', '.nextPages', function (event) {
        if (!isLastListPages) {
            PaintCurrentPage("#6A8FD9");
            if (currentListPages + 1 == fullListPages) {
                let firstNumPage = currentListPages * 5 + 1;
                $(".pages").empty();
                isLastListPages = !isLastListPages;
                for (let i = 0; i < (restCountPages != 0 ? restCountPages : 5); i++) {
                    $('.pages').append(`<button class="nav_menu_button page" value="${firstNumPage + i}">${firstNumPage + i}</button>`);
                }
               
                $('.nextPages').css('background-color', "#bdbbbb");
            }
            else {
                $(".page").each(function () {
                    let num = Number.parseInt($(this).text());
                    $(this).text(num + 5);
                    $(this).val(num + 5);
                });
            }
            currentPage = currentListPages * 5 + 1;
            PaintCurrentPage("#6A8FD9");
            ClickCurrentPage();
            currentListPages++;
            if (currentListPages == 2) $('.previousPages').css('background-color', "#6A8FD9");
        }
    });
});

function GetCurrentPage() {
    let currentHtmlPage = $(".page").filter(function () {
        if ($(this).val() == currentPage)
            return $(this);
    });
    return currentHtmlPage;
};

function PaintCurrentPage(color) {
    GetCurrentPage().css('background-color', color);
}

function ClickCurrentPage() {
    GetCurrentPage().click();
}