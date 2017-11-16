define(['ko', 'lib/requirejs-plugins/lib/text!components/peopleView.html'],
    function (ko, templateString) {
        function peopleView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.people = ko.observableArray();

            self.FirstName = ko.observable();
            self.LastName = ko.observable();
            self.EmailAddress = ko.observable();

            self.showAddDialog = ko.observable(false);

            self.doShowInvitePeople = function () {
                self.showAddDialog(true);
            }

            

            self.doInvitePeople = function () {
                $.ajax({
                    method: 'PUT',
                    url: "api/User/Create",
                    data: {
                        FirstName: self.FirstName(),
                        LastName: self.LastName(),
                        EmailAddress: self.EmailAddress(),
                        IsActive: false,
                    },
                    success: function (data) {
                        self.load();
                        self.showAddDialog(false);
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }

            self.deletePeople = function (people) {
                $.ajax({
                    method: 'DELETE',
                    url: "api/User/DELETE",
                    data: {
                        id: people().Id
                    },
                    success: function (data) {
                        self.load();
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }

            self.doDeactivatePeople = function (people) {
            }
            self.doActivatePeople = function (people) {
            }

            self.load = function () {
                $.ajax({
                    method: 'GET',
                    url: "api/User/List",
                    data: {
                        inviterId: self.currentUser().Id
                    },
                    success: function (data) {
                        self.people(data);
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }

            self.load();
        };

        return { viewModel: peopleView, template: templateString };
    });