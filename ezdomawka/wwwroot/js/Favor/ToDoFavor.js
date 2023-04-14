


$(document).ready(function () {

    $(document).on('click', '#BanUser', function (event) {
        let userId = event.currentTarget.value;
        $('.Content').css('min-height', '10px');
        $('.Content').append(`<form id='form'><div class='text-center'>
                <textarea class='form-control' placeholder='Причина бана' 
                         rows='3' cols='50' required maxlength='200' id='reasonBan'></textarea>
                <input class='form-control mt-2' id='durationBan' value='0' required placeholder='Длительность' type='number'></input>
                <div class='text-center pt-2'><button value='${userId}' id='addBan' class='btn btn-outline-dark'>Забанить</button>
                <button id='CloseModal' class='btn btn-outline-secondary'>Отмена</button></div></form>`);
        $('.Modal').addClass('Active');
    });
    $(document).on('click', '#addBan', function (event) {
        event.preventDefault();
        let reasonBan = $('#reasonBan').val();
        let durationBan = $('#durationBan').val();
        if ($("#form").valid() && reasonBan != '' && durationBan != '') {
            let userId = event.currentTarget.value;
            $('.Content').css('min-height', '200px');
            $('.Content').empty();
            $('.Content').append("<div><div class='loader'></div></div>");
            $.ajax({
                url: '/Admin/BanUser/',
                method: 'post',
                dataType: 'json',
                data: {
                    UserId: userId, Reason: reasonBan, Duration: durationBan
                },
                success: function () {
                    $('.Content').empty();
                    $('.Content').css('min-height', '10px');
                    $('.Content').append(
                        `<div class='text-center' style='width: 300px;'><div class='pb-2'>
                            Пользователь забанен <i class="fa-regular fa-circle-check"></i></div>
                            <button id='CloseModal' class='btn btn-outline-secondary'>Закрыть</button></div>`);
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
        }
    });

    $(document).on('click', '.Modal', function (event) {
        $('.Content').css('min-height', '200px');
        event.stopPropagation();
        $('.Modal').removeClass('Active');
        $('.Content').empty();
    })

    $(document).on('click', '#CloseModal', function (event) {
        $('.Content').css('min-height', '200px');
        event.stopPropagation();
        $('.Modal').removeClass('Active');
        $('.Content').empty();
    })

    $(document).on('click', '.Content', function (event) {
        event.stopPropagation();
    })

    $(document).on('click', '.dislike', function (event) {
        let favorId = event.currentTarget.value;
        $('.Content').css('min-height', '10px');
        $('.Content').append(`<form id='form'><div class='text-center'>
                <textarea class='form-control' placeholder='Причина жалобы' rows='3' cols='50' required maxlength='200' id='reasonReport'></textarea>
                <div class='text-center pt-2'><button value='${favorId}' id='addReport' class='btn btn-outline-dark'>Пожаловаться</button>
                <button id='CloseModal' class='btn btn-outline-secondary'>Отмена</button></div></form>`);
        $('.Modal').addClass('Active');
    });
    $(document).on('click', '#addReport', function (event) {
        let reasonReport = $('#reasonReport').val();
        if ($("#form").valid() && reasonReport != '') {
            let favorId = event.currentTarget.value;
            $('.Content').css('min-height', '200px');
            $('.Content').empty();
            $('.Content').append("<div><div class='loader'></div></div>");
            $.ajax({
                url: '/FavorSolution/AddReport/',
                method: 'post',
                dataType: 'json',
                data: {
                    FavorSolutionId: favorId, Reason: reasonReport
                },
                success: function () {
                    $('.Content').empty();
                    $('.Content').css('min-height', '10px');
                    $('.Content').append(
                        `<div class='text-center' style='width: 300px;'><div class='pb-2'>
                            Жалоба успешно отправлена <i class="fa-regular fa-circle-check"></i></div>
                            <button id='CloseModal' class='btn btn-outline-secondary'>Закрыть</button></div>`);
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
        }
    });
    $(document).on('click', '.checkSubjects', function (event) {
        event.stopPropagation();
        let favorId = event.currentTarget.value;
        NowFavorId = favorId;
        $('.Content').append("<div><div class='loader'></div></div>");
        $('.Modal').addClass('Active');

        $.ajax({
            url: '/FavorSolution/GetFavorSubjects/',
            method: 'get',
            dataType: 'html',
            data: {
                FavorId: favorId, skip: 0, take: 5
            },
            success: function (data) {
                data = JSON.parse(data);
                PageSubjects = 1;
                CountSubjects = data.count;
                $('.Content').empty();
                $('.Content').css('min-height', '10px');
                $('.Content').append(`<div style='width: 300px;' class="modalSubjects"></div>`);
                data.subjects.forEach(function (x) {
                    $('.modalSubjects')
                        .append(`<div class='col-12 pt-1 blockSelSubj'>
                                     <div class='selectedSubject p-3'>
                                         <i class=''>${x.name}</i>
                                     </div>
                              </div>`)
                });
                if (data.count > 5) {
                    $('.modalSubjects')
                        .append(`<div class='col-12 text-center pt-2'>
                                    <button disabled class='backSubj btn btn-outline-dark'>&lt;&lt;</button> 
                                    <button class='nextSubj btn btn-outline-dark'>&gt;&gt;</button>
                              </div>`)
                }
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
    $(document).on('click', '.nextSubj', function () {
        if (PageSubjects * 5 < CountSubjects) {
            $.ajax({
                url: '/FavorSolution/GetFavorSubjects/',
                method: 'get',
                dataType: 'html',
                data: {
                    FavorId: NowFavorId, skip: PageSubjects * 5, take: 5
                },
                success: function (data) {
                    PageSubjects += 1;
                    $('.Content').empty();
                    $('.Content').css('min-height', '10px');
                    $('.Content').append(`<div style='width: 300px;' class="modalSubjects"></div>`);
                    data = JSON.parse(data);
                    data.subjects.forEach(function (x) {
                        $('.modalSubjects')
                            .append(`<div class='col-12 pt-1 blockSelSubj'>
                                     <div class='selectedSubject p-3'>
                                         <i class=''>${x.name}</i>
                                     </div>
                              </div>`)
                    });
                    $('.modalSubjects')
                        .append(`<div class='col-12 buttonsSubj text-center pt-2'>
                                    <button class='backSubj btn btn-outline-dark'>&lt;&lt;</button> 
                              </div>`)
                    if (PageSubjects * 5 >= CountSubjects) {
                        $('.buttonsSubj')
                            .append(`<button disabled class='nextSubj btn btn-outline-dark'>&gt;&gt;</button>`)
                    }
                    else {
                        $('.buttonsSubj')
                            .append(`<button class='nextSubj btn btn-outline-dark'>&gt;&gt;</button>`)
                    }
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
        }
    });

    $(document).on('click', '.backSubj', function (event) {
        if (PageSubjects > 1) {
            $.ajax({
                url: '/FavorSolution/GetFavorSubjects/',
                method: 'get',
                dataType: 'html',
                data: {
                    FavorId: NowFavorId, skip: (PageSubjects * 5 - 10), take: 5
                },
                success: function (data) {
                    PageSubjects -= 1;
                    $('.Content').empty();
                    $('.Content').css('min-height', '10px');
                    $('.Content').append(`<div style='width: 300px;' class="modalSubjects"></div>`);
                    data = JSON.parse(data);
                    data.subjects.forEach(function (x) {
                        $('.modalSubjects')
                            .append(`<div class='col-12 pt-1 blockSelSubj'>
                                     <div class='selectedSubject p-3'>
                                         <i class=''>${x.name}</i>
                                     </div>
                              </div>`)
                    });
                    $('.modalSubjects')
                        .append(`<div class='col-12 buttonsSubj text-center pt-2'> 
                                    <button class='nextSubj btn btn-outline-dark'>&gt;&gt;</button>
                              </div>`)
                    if (PageSubjects > 1) {
                        $('.buttonsSubj')
                            .prepend(`<button class='backSubj btn btn-outline-dark'>&lt;&lt;</button>`)
                    }
                    else {
                        $('.buttonsSubj')
                            .prepend(`<button disabled class='backSubj btn btn-outline-dark'>&lt;&lt;</button>`)
                    }
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
        }
    });

});