define(['ko', 'lib/requirejs-plugins/lib/text!components/projectsView.html'],
    function (ko, templateString) {
        function projectsView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.projects = ko.observableArray();
            self.Name = ko.observable();
            self.showAddDialog = ko.observable(false);

            self.doShowAddProject = function () {
                self.showAddDialog(true);
            }

            self.doAddProject = function () {
                $.ajax({
                    method: 'PUT',
                    url: "api/Project/Create",
                    data: {
                        Name: self.Name(),
                        IsActive: true,
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

            self.doDeactivateProject = function (projectId) {
            }

            self.deleteProject = function (project) {
                $.ajax({
                    method: 'GET',
                    url: "api/Project/Delete",
                    data: {
                        projectId: project.Id
                    },
                    success: function (data) {
                        self.load();
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }

            self.doActivateProject = function (projectId) {
            }

            self.load = function() {
                $.ajax({
                    method: 'GET',
                    url: "api/Project/List",
                    success: function (data) {
                        self.projects(data);
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }

            self.load();
        };

        return { viewModel: projectsView, template: templateString };
    });