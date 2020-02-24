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
	accountNumber: new InputData('Account Number:', 'Please select this or card number'),
	cardNumber: new InputData('Card Number:'),
	selected: -1,
	validated: false,
	emailChecked: false,
	faxChecked: false,
	error: '',
	CIF: '',
	name: '',
	Statement: false,
	FDConfirmation: false,
	Remittance: false,
	JointAccount: false,
	Investments: false,
	PrimaryEmail: '',
	SecondaryEmail: '',
	FaxNumber: '',
	inProgress: false
};
var app = new Vue({
	data: data,
	el: "#app",
	methods: {
		save: function () {
			app.inProgress = true;
			axios.post('/Registration/UpdateInformation', {
				info: {
					CIF: app.CIF,
					Statement: app.Statement,
					FDConfirmation: app.FDConfirmation,
					Remittance: app.Remittance,
					JointAccount: app.JointAccount,
					Investments: app.Investments,
					HasEmail: app.emailChecked,
					PrimaryEmail: app.PrimaryEmail,
					SecondaryEmail: app.SecondaryEmail,
					HasFax: app.faxChecked,
					FaxNumber: app.FaxNumber
				}
			}, function () {
			}).then(function (resp) {
				app.inProgress = false;
				var d = resp.data;
				if (d.result) {
					alert("Information updated successfully")
				} else {
					app.error = d.Error;
				}
			});
		},
		validate: function () {
			axios.post('/Registration/Validate', {
				accountNumber: app.selected == 0 ? app.accountNumber.value : '',
				cardNumber: app.selected == 1 ? app.cardNumber.value : ''
			}).then(function (response) {
				var d = response.data;
				app.reset();
				debugger;
				if (response.data.Validated) {
					app.validated = true;
					app.CIF = d.CIF;
					app.name = d.Name;
					app.Statement = d.Statement;
					app.FDConfirmation = d.FDConfirmation;
					app.Remittance = d.Remittance;
					app.JointAccount = d.JointAccount;
					app.Investments = d.Investments;
					app.emailChecked = d.HasEmail;
					app.PrimaryEmail = d.PrimaryEmail;
					app.SecondaryEmail = d.SecondaryEmail;
					app.FaxNumber = d.FaxNumber;
					app.faxChecked = d.HasFax;
				} else {
					app.error = d.Error;
					FadeFunction(function () { app.error = ''; });
				}
			});
		},
		reset() {
			app.validated = false;
			app.emilChecked = false;
			app.faxChecked = false;
			app.error = '';
			app.CIF = '';
			app.name = '';
			app.Statement = false;
			app.FDConfirmation = false;
			app.Remittance = false;
			app.JointAccount = false;
			app.Investments = false;
			app.emailChecked = false;
			app.faxChecked = false;
			app.PrimaryEmail = '';
			app.SecondaryEmail = '';
			app.FaxNumber = '';
		}
	}
});

function FadeFunction(fx) {
	setTimeout(function () { fx(); }, 3000);
}