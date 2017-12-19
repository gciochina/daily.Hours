define(['ko', 'lib/requirejs-plugins/lib/text!components/peopleView.html'],
    function (ko, templateString) {
        function peopleView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.people = ko.observableArray();
            self.projects = ko.observableArray();

            self.FirstName = ko.observable().extend({ required: true });
            self.LastName = ko.observable().extend({ required: true });
            self.EmailAddress = ko.observable().extend({ required: true });
            self.Project = ko.observable().extend({ required: true });

            self.showAddDialog = ko.observable(false);

            self.doShowInvitePeople = function () {
                $.ajax({
                    method: 'GET',
                    url: "api/Project/List",
                    success: function (data) {
                        self.FirstName('');
                        self.LastName('');
                        self.showAddDialog(true);
                        self.projects(data);
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }

            self.doInvitePeople = function () {
                self.validationErrors = ko.validation.group(this, { deep: true })
                if (self.validationErrors().length > 0) {
                    self.validationErrors.showAllMessages(true);
                    return;
                }

                $.ajax({
                    method: 'PUT',
                    url: "api/User/Create",
                    data: {
                        FirstName: self.FirstName(),
                        LastName: self.LastName(),
                        EmailAddress: self.EmailAddress(),
                        Projects: [ self.Project()],
                        IsActive: false,
                    },
                    success: function (data) {
                        self.load();
                        self.showAddDialog(false);
                    },
                    error: function (error) {
                        HandleError(error);
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
                        HandleError(error);
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
                        HandleError(error);
                    }
                });
            }

            self.load();
        };

        return { viewModel: peopleView, template: templateString };
    });