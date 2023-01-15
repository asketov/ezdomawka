$(document).ready(function () {
    let ThemeId = null;
    let SubjectId = null;
    let MinPrice = null;
    let MaxPrice = null;

    $("#findFavors").click(function () {
        let selectedThemeId = $("#themes option:selected").val();
        let selectedSubjectId = $("#subjects option:selected").val();
        let minPrice = Number.parseInt($("#minPrice").val().split(' ')[0]);
        let maxPrice = Number.parseInt($("#maxPrice").val().split(' ')[0]);
        $('#FavorsWithPagination').empty();
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
    });


    $(document).on('click', '.page', function (event) {
        let skip = (Number.parseInt(event.currentTarget.value) - 1) * 10;
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
                window.scrollTo(0, 0);
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
    priceInput = document.querySelectorAll(".price-input input"),
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
    
    
});

