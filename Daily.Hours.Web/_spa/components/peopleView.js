define(['ko', 'lib/requirejs-plugins/lib/text!components/peopleView.html'],
    function (ko, templateString) {
        function peopleView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.people = ko.observableArray();

            self.doAddPeople = function () {
            }

            self.doDeactivatePeople = function (peopleId) {
            }

            self.deletePeople = function (peopleId) {
                $.ajax({
                    method: 'DELETE',
                    url: "api/Users/DELETE",
                    data: {
                        id: peopleId
                    },
                    success: function (data) {
                        self.load();
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }

            self.doActivatePeople = function (peopleId) {
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