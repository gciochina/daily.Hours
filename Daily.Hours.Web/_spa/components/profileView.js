define(['ko', 'lib/requirejs-plugins/lib/text!components/profileView.html', 'toastr'],
    function (ko, templateString, toastr) {
        function profileView(params) {
            var self = this;

            self.navModel = params.navModel;
            self.currentUser = params.currentUser;

            self.emailAddress = ko.observable(self.currentUser().EmailAddress).extend({ required: true });
            self.firstName = ko.observable(self.currentUser().FirstName).extend({ required: true });
            self.lastName = ko.observable(self.currentUser().LastName).extend({ required: true });
            self.password = ko.observable(self.currentUser().Password).extend({ required: true });

            self.doUpdate = function () {
                self.validationErrors = ko.validation.group(this, { deep: true })
                if (self.validationErrors().length > 0) {
                    self.validationErrors.showAllMessages(true);
                    return;
                }

                $.ajax({
                    method: 'POST',
                    url: "api/User/Update",
                    data: {
                        Id: self.currentUser().Id,
                        EmailAddress: self.emailAddress(),
                        FirstName: self.firstName(),
                        LastName: self.lastName(),
                        Password: self.password()
                    },
                    success: function (data) {
                        if (data != null) {
                            toastr.info("Your profile has been updated", "Done");
                            self.currentUser(data);
                        }
                        else {
                            toastr.warning("Something's not right", "Hmmm");
                        }
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            };
        };

        return { viewModel: profileView, template: templateString };
    });