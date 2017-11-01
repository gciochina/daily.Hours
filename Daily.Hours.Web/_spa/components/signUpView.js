define(['ko', 'lib/requirejs-plugins/lib/text!components/signUpView.html'],
    function (ko, templateString) {
        function signUpView(params) {
            var self = this;

            self.firstName = ko.observable().extend({ required: true });
            self.lastName = ko.observable().extend({ required: true });
            self.userName = ko.observable().extend({ required: true });
            self.passWord = ko.observable().extend({ required: true, minLength: 6 });
            self.emailAddress = ko.observable().extend({ required: true, email: true });

            self.navModel = params.navModel;
            self.currentUser = params.currentUser;
            self.validationErrors = ko.validation.group(this, { deep: true })
            self.validationErrors.showAllMessages();

            self.doRegisterUser = function () {
                if (self.validationErrors().length > 0)
                    return;

                $.ajax({
                    method: 'POST',
                    url: "api/User/Register",
                    data: {
                        firstName: self.firstName(),
                        lastName: self.lastName(),
                        userName: self.userName(),
                        passWord: self.passWord(),
                        emailAddress: self.emailAddress()
                    },
                    success: function (data) {
                        self.navModel('welcomeView');
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }
        };

        return { viewModel: signUpView, template: templateString };
    });