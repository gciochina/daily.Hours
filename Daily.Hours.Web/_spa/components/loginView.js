define(['ko', 'lib/requirejs-plugins/lib/text!components/loginView.html', 'toastr'],
    function (ko, templateString, toastr) {
        function loginView(params) {
            var self = this;

            self.emailAddress = ko.observable().extend({ required: true });
            self.password = ko.observable().extend({ required: true });
            self.rememberMe = ko.observable(false);

            self.navModel = params.navModel;
            self.currentUser = params.currentUser;

            self.goSignup = function () {
                self.navModel('signUpView');
            }

            self.doLogin = function () {
                self.validationErrors = ko.validation.group(this, { deep: true })
                if (self.validationErrors().length > 0) {
                    self.validationErrors.showAllMessages(true);
                    return;
                }

                $.ajax({
                    method: 'POST',
                    url: "api/User/Login?rememberMe=" + self.rememberMe(),
                    data: {
                        EmailAddress: self.emailAddress(),
                        Password: self.password()
                    },
                    success: function (data) {
                        if (data == null) {
                            toastr.warning("User not found/ incorrect password", "Failed")
                        }
                        else {
                            self.currentUser(data);
                            self.navModel('dayView');
                            toastr.info(data.FirstName + " " + data.LastName, "Welcome back");
                        }
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            };
        };

        return { viewModel: loginView, template: templateString };
    });