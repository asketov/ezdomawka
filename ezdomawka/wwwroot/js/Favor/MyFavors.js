

$(document).ready(function () {

	$(document).on('click', '#reloadFavor', function (event) {
		let FavorId = event.currentTarget.value;
			$.ajax({
				url: '/FavorSolution/UpdateFavorDate',
				method: 'post',
				dataType: 'json',
				data: {
					favorId: FavorId
				},
				success: function () {
					location.reload();
				},
				statusCode: {
					400: function () { // выполнить функцию если код ответа HTTP 400
						alert("Неправильный запрос");
					},
					404: function () { // выполнить функцию если код ответа HTTP 404
						alert("Страница не найдена");
					},
					406: function () {
						$('.Content').empty();
						$('.Content').css('min-height', '10px');
						$('.Content').append(
							`<div class='text-center' style='width: 300px;'><div class='pb-2'>
                            Достигнут лимит в обновлении услуг</div>
                            <button id='CloseModal' class='btn btn-outline-secondary'>Закрыть</button></div>`);
						$('.Modal').addClass('Active');
					}
				}
			});
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
});