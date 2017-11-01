define(['ko', 'lib/requirejs-plugins/lib/text!components/signUpView.html'],
    function (ko, templateString) {
        function signUpView(params) {
            var self = this;

            self.firstName = ko.observable("");
            self.lastName = ko.observable("");
            self.userName = ko.observable("");
            self.passWord = ko.observable("");
            self.emailAddress = ko.observable("");

            self.navModel = params.navModel;
            self.currentUser = params.user;

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

        return { viewModel: signUpView, template: templateString };
    });