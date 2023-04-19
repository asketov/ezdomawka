
$(document).ready(function () {
    let ThemeId = $("#themes option:selected").val();
    let SubjectId = $("#subjects option:selected").val();
    let MinPrice = Number.parseInt($("#minPrice").val().split(' ')[0]);
    let MaxPrice = Number.parseInt($("#maxPrice").val().split(' ')[0]);
    let PageSubjects = 0;
    let CountSubjects;
    let NowFavorId;

    $("#findFavors").click(function () {
        let selectedThemeId = $("#themes option:selected").val();
        let selectedSubjectId = $("#subjects option:selected").val();
        let minPrice = Number.parseInt($("#minPrice").val().split(' ')[0]);
        let maxPrice = Number.parseInt($("#maxPrice").val().split(' ')[0]);
        $('#FavorsWithPagination').append("<div class='loader'></div>");
            $.ajax({
                url: '/FavorSolution/FindFavors/',
                method: 'get',
                dataType: 'html',
                data: {
                    ThemeId: selectedThemeId, SubjectId: selectedSubjectId,
                    MinPrice: minPrice, MaxPrice: maxPrice
                },
                success: function (data) {
                    $('#FavorsWithPagination').empty();
                    $('#FavorsWithPagination').append(data);
                    ThemeId = selectedThemeId;
                    SubjectId = selectedSubjectId;
                    MinPrice = minPrice;
                    MaxPrice = maxPrice;
                    location.href = "#header";
                },
                statusCode: {
                    400: function () { // выполнить функцию если код ответа HTTP 400
                        $('#FavorsWithPagination').empty();
                        $('#FavorsWithPagination').append(`<div class='d-flex justify-content-center pt-3'>
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
        $('#favorSolutions').append("<div class='pt-4'><div class='loader'></div></div>");
        $.ajax({
            url: '/FavorSolution/GetSolutions/',
            method: 'get',
            dataType: 'html',
            data: {
                ThemeId: ThemeId, SubjectId: SubjectId, Take: 10,
                Skip: skip, MinPrice: MinPrice, MaxPrice: MaxPrice
            },
            success: function (data) {
                
                $('#favorSolutions').empty();
                $('#favorSolutions').replaceWith(data);
                location.href = "#header";
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
    })

    const rangeInput = document.querySelectorAll(".range-input input"), 
    priceInput = document.querySelectorAll(".price_range_numeric input"),
    range = document.querySelector(".slider .progress");
    let priceGap = 1;
    priceInput.forEach(input =>{
        input.addEventListener("input", e =>{
            let minPrice = parseInt(priceInput[0].value),
                maxPrice = parseInt(priceInput[1].value);

            if((maxPrice - minPrice >= priceGap) && maxPrice <= rangeInput[1].max){
                if(e.target.className === "input-min"){
                    rangeInput[0].value = minPrice;
                    range.style.left = ((minPrice / rangeInput[0].max) * 100) + "%";
                }else{
                    rangeInput[1].value = maxPrice;
                    range.style.right = 100 - (maxPrice / rangeInput[1].max) * 100 + "%";
                }
            }
        });
    });
    rangeInput.forEach(input =>{
        input.addEventListener("input", e =>{
            let minVal = parseInt(rangeInput[0].value),
                maxVal = parseInt(rangeInput[1].value);
            if((maxVal - minVal) < priceGap){
                if(e.target.className === "range-min"){
                    rangeInput[0].value = maxVal - priceGap;
                }else{
                    rangeInput[1].value = minVal + priceGap;
                }
            }else{
                priceInput[0].value = minVal + ' Р';
                priceInput[1].value = maxVal + ' Р';
                range.style.left = ((minVal / rangeInput[0].max) * 100) + "%";
                range.style.right = 100 - (maxVal / rangeInput[1].max) * 100 + "%";
            }
        });
    });

   

    $(".serviceListSearch").on('change', function () {
        let selectedThemeId = $("#themes option:selected").val();
        let selectedSubjectId = $("#subjects option:selected").val();
        let minPrice = Number.parseInt($("#minPrice").val().split(' ')[0]);
        let maxPrice = Number.parseInt($("#maxPrice").val().split(' ')[0]);
        $('#FavorsWithPagination').append("<div class='loader'></div>");
        $.ajax({
            url: '/FavorSolution/FindFavors/',
            method: 'get',
            dataType: 'html',
            data: {
                ThemeId: selectedThemeId, SubjectId: selectedSubjectId,
                MinPrice: minPrice, MaxPrice: maxPrice
            },
            success: function (data) {
                $('#FavorsWithPagination').empty();
                $('#FavorsWithPagination').append(data);
                ThemeId = selectedThemeId;
                SubjectId = selectedSubjectId;
                MinPrice = minPrice;
                MaxPrice = maxPrice;
                window.scrollTo(0, 0);
                addSubjectToCard();
            },
            statusCode: {
                400: function () { // выполнить функцию если код ответа HTTP 400
                    $('#FavorsWithPagination').empty();
                    $('#FavorsWithPagination').append(`<div class='d-flex justify-content-center pt-3'>
                                            <div class='text-danger'>Запрос введён неверно</div></div>`);
                },
                404: function () { // выполнить функцию если код ответа HTTP 404
                    alert("Страница не найдена");
                }
            }
        });
    });
});