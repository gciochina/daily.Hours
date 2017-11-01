define(['ko', 'lib/requirejs-plugins/lib/text!components/loginView.html'],
    function (ko, templateString) {
        function loginView(params) {
            var self = this;

            self.firstName = ko.observable("");
            self.lastName = ko.observable("");
            self.userName = ko.observable("");
            self.passWord = ko.observable("");
            self.emailAddress = ko.observable("");

            self.navModel = params.navModel;
            self.user = params.user;

            self.doSignup = function () {
                self.navModel('signUpView');
            }

            self.doRegisterUser = function () {
                $.ajax({
                    method: 'POST',
                    url: "api/User/Register",
                    data: {
                        firstName: self.firstName(),
                        lastName: self.lastName(),
                        userName: self.userName(),
                        password: self.passWord(),
                        emailAddress: self.emailAddress()
                    },
                    success: function (data) {
                        self.user(data);
                        self.navModel('welcomeView');
                    },
                    fail: function (error) {
                        alert(error);
                    }
                });
            }
        };

        return { viewModel: loginView, template: templateString };
    });