
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
    function addSubjectToCard() {
        let val = $("#subjects option:selected").html();
        if (val != "Любой предмет") {
            $('.subjects').append(
                `<div class='pt-2 blockSelSubj'>
                            <div class='selectedSubject p-3'>
                                   <i class=''>${val} </i>
                            </div>
                </div>`);
        }
    }

    $(document).on('click', '.checkSubjects', function (event) {
        event.stopPropagation();
        let favorId = event.currentTarget.value;
        $('.Content').append("<div><div class='loader'></div></div>");
        $('.Modal').addClass('Active');
        $.ajax({
            url: '/FavorSolution/GetFavorSubjects/',
            method: 'get',
            dataType: 'html',
            data: {
                FavorId: favorId
            },
            success: function (data) {
                $('.Content').empty();
                $('.Content').css('min-height', '10px');
                $('.Content').append(`<div style='width: 300px;' class="modalSubjects"></div>`);
                data = JSON.parse(data);
                data.forEach(function (x) {
                        $('.modalSubjects')
                        .append(`<div class='col-12 pt-1 blockSelSubj'>
                                     <div class='selectedSubject p-3'>
                                         <i class=''>${x.name}</i>
                                     </div>
                              </div>`)
                    }
                );
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

    $(document).on('click', '.Modal', function (event) {
        event.stopPropagation();
        $('.Modal').removeClass('Active');
        $('.Content').empty();
    })

    $(document).on('click', '.dislike', function (event) {
        event.stopPropagation();
        let favorId = event.currentTarget.value;
        $('.Content').css('min-height', '10px');
        $('.Content').append(`<div class='text-center'>Вы уверены, что хотите пожаловаться?</div>
                <div class='text-center pt-2'><button value='${favorId}' id='addReport' class='btn btn-outline-dark'>Пожаловаться</button>
                <button class='btn btn-outline-secondary'>Отмена</button></div>`);
        $('.Modal').addClass('Active');
    });
    $(document).on('click', '#addReport', function (event) {
        let favorId = event.currentTarget.value;
        $('.Content').css('min-height', '200px');
        $('.Content').empty();
        $('.Content').append("<div><div class='loader'></div></div>");
        $.ajax({
            url: '/FavorSolution/AddReport/',
            method: 'post',
            dataType: 'json',
            data: {
                favorId: favorId
            },
            success: function () {
                $('.Content').empty();
                $('.Content').css('min-height', '10px');
                $('.Content').append(
                    `<div style='width: 300px;'>Жалоба успешно отправлена <i class="fa-regular fa-circle-check"></i></div>`);
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
});

