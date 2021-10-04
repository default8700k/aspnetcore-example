$(document).ready(function() {
	let $order_form = $('form#form-order');
	let $order_submit = $order_form.find('button[type="submit"]');

	let $order_result = $('span#order-result');
	$order_form.submit(function(e) {
		$order_submit.attr('disabled', true);

		$.ajax({
			url: $order_form.attr('action'),
			type: $order_form.attr('method'),
			data: $order_form.serialize()
		}).done(function() {
			$modalInfo_title.text('Оформить заявку');
			$modalInfo_content.text('Заявка успешно отправлена.');

			$order_result.text('Заявка успешно отправлена.');
		}).fail(function(xhr) {
			const errors = {
				400: 'Неверный формат данных.',
				404: 'Не удалось отправить запрос к серверу.',
				500: 'Внутренняя ошибка сервера.',
				999: 'Неизвестный номер ошибки.'
			};

			if (errors[xhr.status] === undefined) {
				xhr.status = 999;
			}

			$modalInfo_title.text('Произошла ошибка');
			$modalInfo_content.text(`Не удалось оставить заявку. ${errors[xhr.status]}`);

			$order_submit.removeAttr('disabled');
			$order_result.text(`Не удалось оставить заявку. ${errors[xhr.status]}`);
		}).always(function() {
			$modalInfo.modal('show');
		});

		e.preventDefault();
	});
});