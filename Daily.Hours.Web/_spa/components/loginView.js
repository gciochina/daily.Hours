define(['ko', 'lib/requirejs-plugins/lib/text!components/loginView.html', 'toastr'],
    function (ko, templateString, toastr) {
        function loginView(params) {
            var self = this;

            self.userName = ko.observable("");
            self.password = ko.observable("");

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
                        self.currentUser(data);
                        self.navModel('mainView');
                        toastr.info("Welcome back" + data.FirstName + " " + data.LastName);
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }
        };

        return { viewModel: loginView, template: templateString };
    });