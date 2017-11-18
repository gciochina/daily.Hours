define(['ko', 'lib/requirejs-plugins/lib/text!components/signUpView.html'],
    function (ko, templateString) {
        function signUpView(params) {
            var self = this;

            self.firstName = ko.observable().extend({ required: true });
            self.lastName = ko.observable().extend({ required: true });
            self.passWord = ko.observable().extend({ required: true, minLength: 6 });
            self.emailAddress = ko.observable().extend({ required: true, email: true });

            self.navModel = params.navModel;
            self.currentUser = params.currentUser;

            self.goLogin = function () {
                self.navModel("loginView");
            }

            self.doRegisterUser = function () {
                self.validationErrors = ko.validation.group(this, { deep: true })
                if (self.validationErrors().length > 0) {
                    self.validationErrors.showAllMessages(true);
                    return;
                }

                $.ajax({
                    method: 'POST',
                    url: "api/User/Register",
                    data: {
                        firstName: self.firstName(),
                        lastName: self.lastName(),
                        passWord: self.passWord(),
                        emailAddress: self.emailAddress()
                    },
                    success: function (data) {
                        self.navModel('welcomeView');
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }
        };

        return { viewModel: signUpView, template: templateString };
    });