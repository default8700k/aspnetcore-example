let chars = [
	'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
	'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
	'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
	'y', 'z'
];

let fieldsCount = 0;
let fieldsLimit = chars.length;

$(document).ready(function() {
	let $fields = $('div#fields');
	$fields.find('input:not(:disabled)').each(function() {
		$(this).change(function() {
			let $input = $(this);
			$input.val($input.val().toFormat());

			let char = $input.attr('id').split('-')[1];

			let length = getInputValue('length', char).toFormat().toFloat();
			let width = getInputValue('width', char).toFormat().toFloat();
			let height = getInputValue('height', char).toFormat().toFloat();

			let volume = length * width * height;
			setInputValue('volume', char, volume.toFixed(2));
		});
	});

	let $template = $fields.children().clone(true);
	let $template_button = $template.find('button');
	$template_button.removeClass('btn-primary');
	$template_button.addClass('btn-danger');
	$template_button.text('Удалить');

	let $button_append = $fields.find('button');
	$button_append.click(function() {
		if (fieldsCount === fieldsLimit) {
			return console.error('limit');
		}

		let char = chars.shift();

		let $templateCloned = $template.clone(true);
		$templateCloned.find('label').each(function() {
			let $label = $(this);
			let id = $label.attr('for');
			$label.attr('for', `${id}-${char}`);
		});

		$templateCloned.find('input').each(function() {
			let $input = $(this);
			let id = $input.attr('id');
			$input.attr('id', `${id}-${char}`);
		});

		$templateCloned.find('button').click(function() {
			$templateCloned.remove();
			fieldsCount--;

			chars.push(char);
			if ($button_append.attr('disabled') !== undefined) {
				$button_append.removeAttr('disabled');
			}
		});

		fieldsCount++;
		if (fieldsCount === fieldsLimit) {
			$button_append.attr('disabled', true);
		}

		$fields.append($templateCloned);
	});

	let $isPickup = $('input#isPickup');
	let $isDelivery = $('input#isDelivery');

	$('button#calculate').click(function() {
		let from = $('select#city-from option:selected').val();
		let to = $('select#city-to option:selected').val();

		let route = `${from}-${to}`;
		if (routes[route] !== true) {
			$modalInfo_title.text('Калькулятор');
			$modalInfo_content.text('Не удалось найти маршрут.');
			return $modalInfo.modal('show');
		}

		let isPickup = $isPickup.is(':checked');
		let isDelivery = $isDelivery.is(':checked');

		let price = 0;
		$fields.find('input[id^="length"]').each(function() {
			let char = $(this).attr('id').split[1];

			let weight = getInputValue('weight', char).toFloat();
			let volume = getInputValue('volume', char).toFloat();
			if (weight === 0 && volume === 0) {
				return true;
			}

			for (let action of [
				() => {
					for (let params of rates.small.prices) {
						if (weight < params.maxWeight &&
							volume < params.maxVolume) {
							return {
								isFixed: rates.small.isFixed,
								value: params.values[route]
							};
						}
					}
				},
				() => {
					if (weight > volume * 220) { return undefined; }
					for (let params of rates.default.prices) {
						if (weight < params.maxWeight &&
							volume < params.maxVolume) {
							return {
								isFixed: rates.default.isFixed,
								value: params.values[route] * volume
							};
						}
					}
				},
				() => {
					for (let params of rates.weight.prices) {
						if (weight < params.maxWeight) {
							return {
								isFixed: rates.weight.isFixed,
								value: params.values[route]
							};
						}
					}
				}
			]) {
				let params = action.call();
				if (params !== undefined) {
					price += params.isFixed === true ? params.value : params.value * weight;
					break;
				}
			}

			if (price === 0) {
				$modalInfo_title.text('Калькулятор');
				$modalInfo_content.text('Не удалось посчитать примерную стоимость доставки.');
				return $modalInfo.modal('show');
			}

			if (isPickup === true || isDelivery === true) {
				for (let params of services[route]) {
					if (weight < params.maxWeight && volume < params.maxVolume) {
						if (isPickup === true) { price += params.prices.pickup; }
						if (isDelivery === true) { price += params.prices.delivery; }
						break;
					}
				}
			}
		});

		price = price * 1.1;
		price = price * 0.8;

		$('#calculator-result').text(price.toFixed(2));
	});
});

function getInput(name, char) {
	let id = char === undefined ? name : name + `-${char}`;
	return $(`input[id^="${id}"]`);
}

function getInputValue(name, char) {
	let $input = getInput(name, char);
	return $input.val();
}

function setInputValue(name, char, value) {
	let $input = getInput(name, char);
	$input.val(value);
}

String.prototype.toFloat = function(defaultValue = 0) {
	let convertedValue = parseFloat(this);
	return isNaN(convertedValue) ? defaultValue : convertedValue;
}

String.prototype.toFormat = function() {
	let value = this;
	value = value.replace(',', '.');
	value = value.replace(/[^0-9\.]/g, '');
	return value;
}