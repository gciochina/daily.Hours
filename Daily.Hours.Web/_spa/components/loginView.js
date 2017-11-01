define(['ko', 'lib/requirejs-plugins/lib/text!components/loginView.html', 'toastr'],
    function (ko, templateString, toastr) {
        function loginView(params) {
            var self = this;

            self.userName = ko.observable().extend({ required: true });
            self.password = ko.observable().extend({ required: true });

            self.navModel = params.navModel;
            self.currentUser = params.currentUser;

            self.doSignup = function () {
                self.navModel('signUpView');
            }

            self.doLogin = function () {
                $.ajax({
                    method: 'POST',
                    url: "api/User/Login",
                    data: {
                        userName: self.userName(),
                        password: self.password(),
                    },
                    success: function (data) {
                        if (data == null) {
                            toastr.warning("User not found/ incorrect password", "Failed")
                        }
                        else {
                            self.currentUser(data);
                            self.navModel('mainView');
                            toastr.info(data.FirstName + " " + data.LastName, "Welcome back");
                        }
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }
        };

        return { viewModel: loginView, template: templateString };
    });