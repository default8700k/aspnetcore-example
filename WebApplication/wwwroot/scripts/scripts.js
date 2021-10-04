let $modalInfo = null;
let $modalInfo_title = null;
let $modalInfo_content = null;

$(document).ready(function() {
	let link = $(location).attr('pathname').split('/')[1];
	$(`a[href="/${link}"]`).addClass('active');

	$modalInfo = $('div#modal-info');
	$modalInfo_title = $modalInfo.find('.modal-title');
	$modalInfo_content = $modalInfo.find('.modal-body');

	let $modalCallback = $('div#modal-callback');
	let $modalCallback_form = $modalCallback.find('form');
	let $modalCallback_submit = $modalCallback_form.find('button[type="submit"]');

	$modalCallback_form.submit(function(e) {
		$modalCallback_submit.attr('disabled', true);

		$.ajax({
			url: $modalCallback_form.attr('action'),
			type: $modalCallback_form.attr('method'),
			data: $modalCallback_form.serialize()
		}).done(function() {
			$modalCallback.bind('hidden.bs.modal', function() {
				$(this).unbind();
				$modalCallback_form.trigger('reset');
				$modalInfo.modal('show');
			});

			$modalInfo_title.text('Обратный звонок');
			$modalInfo_content.text('Заявка на обратный звонок успешно отправлена.');
			$modalCallback.modal('hide');
		}).fail(function(xhr) {
			$modalCallback.bind('hidden.bs.modal', function() {
				$(this).unbind();
				$modalInfo.modal('show');
			});

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
			$modalInfo_content.text(`Не удалось оставить заявку на обратный звонок. ${errors[xhr.status]}`);
			$modalCallback.modal('hide');
		}).always(function() {
			$modalCallback_submit.removeAttr('disabled');
		});

		e.preventDefault();
	});
});