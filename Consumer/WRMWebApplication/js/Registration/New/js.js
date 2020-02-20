function _instanceof(left, right) { if (right != null && typeof Symbol !== "undefined" && right[Symbol.hasInstance]) { return !!right[Symbol.hasInstance](left); } else { return left instanceof right; } }

function _classCallCheck(instance, Constructor) { if (!_instanceof(instance, Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

var InputData = function InputData(title) {
	var desc = arguments.length > 0 && arguments[1] !== undefined ? arguments[1] : '';
	_classCallCheck(this, InputData);

	_defineProperty(this, "value", '');
	_defineProperty(this, "enabled", 0);
	_defineProperty(this, "selected", 0);
	_defineProperty(this, "description", desc);
	_defineProperty(this, "title", title);
	_defineProperty(this, "error", '');

	//_defineProperty(this, "fun", function () {
	//	return d + 150;
	//});
};

var data = {
	accountNumber: new InputData('Account Number:','Please select this or card number'),
	cardNumber: new InputData('Card Number:'),
	selected: -1
};
var app = new Vue({
	data: data,
	el: "#app"
});