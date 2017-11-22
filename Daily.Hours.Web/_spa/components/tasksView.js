define(['ko', 'lib/requirejs-plugins/lib/text!components/tasksView.html'],
    function (ko, templateString) {
        function tasksView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.tasks = ko.observableArray();
            self.projects = ko.observableArray();

            self.Id = ko.observable();
            self.Project = ko.observable().extend({ required: true });
            self.Name = ko.observable().extend({ required: true });
            self.showAddDialog = ko.observable(false);
            self.showEditDialog = ko.observable(false);

            self.doShowAddTask = function () {
                $.ajax({
                    method: 'GET',
                    url: "api/Project/List",
                    success: function (data) {
                        self.Id(0);
                        self.Name('');
                        self.showAddDialog(true);
                        self.projects(data);
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }

            self.doEditTask = function (task) {
                self.Id(task.Id);
                self.Name(task.Name);
                self.projects([{ Id: task.ProjectId }]);
                self.Project({ Id: task.ProjectId });
                self.showEditDialog(true);
            }

            self.doSaveAddTask = function () {
                self.validationErrors = ko.validation.group(this, { deep: true })
                if (self.validationErrors().length > 0) {
                    self.validationErrors.showAllMessages(true);
                    return;
                }
                $.ajax({
                    method: 'GET',
                    url: "api/Task/Create",
                    data: {
                        Name: self.Name(),
                        ProjectId: self.Project().Id
                    },
                    success: function (data) {
                        self.load();
                        self.showAddDialog(false);
                        self.showEditDialog(false);
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }

            self.doSaveEditTask = function () {
                self.validationErrors = ko.validation.group(this, { deep: true })
                if (self.validationErrors().length > 0) {
                    self.validationErrors.showAllMessages(true);
                    return;
                }
                $.ajax({
                    method: 'POST',
                    url: "api/Task/Update",
                    data: {
                        Id: self.Id(),
                        Name: self.Name(),
                        ProjectId: self.Project().Id
                    },
                    success: function (data) {
                        self.load();
                        self.showAddDialog(false);
                        self.showEditDialog(false);
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }

            self.doDeleteTask = function (task) {
                $.ajax({
                    method: 'GET',
                    url: "api/Task/Delete",
                    data: {
                        taskId: task.Id
                    },
                    success: function (data) {
                        self.load();
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }

            self.load = function () {
                $.ajax({
                    method: 'GET',
                    url: "api/Task/List",
                    success: function (data) {
                        self.tasks(data);
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }
            self.load();
        };

        return { viewModel: tasksView, template: templateString };
    });