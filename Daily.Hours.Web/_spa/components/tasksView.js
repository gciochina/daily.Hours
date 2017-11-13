define(['ko', 'lib/requirejs-plugins/lib/text!components/tasksView.html'],
    function (ko, templateString) {
        function tasksView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.tasks = ko.observableArray();
            self.projects = ko.observableArray();

            self.Project = ko.observable();
            self.Name = ko.observable();
            self.showAddDialog = ko.observable(false);

            self.doShowAddTask = function () {
                $.ajax({
                    method: 'GET',
                    url: "api/Project/List",
                    success: function (data) {
                        self.showAddDialog(true);
                        self.projects(data);
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
                
            }

            self.doAddTask = function () {
                $.ajax({
                    method: 'PUT',
                    url: "api/Task/Create",
                    data: {
                        Name: self.Name(),
                        Project: self.Project()
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

            self.doDeleteTask = function (taskId) {
                $.ajax({
                    method: 'DELETE',
                    url: "api/Task/DELETE",
                    data: {
                        id: taskId
                    },
                    success: function (data) {
                        self.load();
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }

            self.load = function () {
                $.ajax({
                    method: 'GET',
                    url: "api/Task/List",
                    data: {
                        userId: self.currentUser().Id
                    },
                    success: function (data) {
                        self.tasks(data);
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }
            self.load();
        };

        return { viewModel: tasksView, template: templateString };
    });