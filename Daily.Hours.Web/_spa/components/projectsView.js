define(['ko', 'lib/requirejs-plugins/lib/text!components/projectsView.html'],
    function (ko, templateString) {
        function projectsView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.projects = ko.observableArray();

            self.doAddProject = function () {
            }

            self.doDeactivateProject = function (projectId) {
            }

            self.deleteProject = function (projectId) {
                $.ajax({
                    method: 'DELETE',
                    url: "api/Project/DELETE",
                    data: {
                        id: projectId
                    },
                    success: function (data) {
                        self.load();
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }

            self.doActivateProject = function (projectId) {
            }

            self.load = function() {
                $.ajax({
                    method: 'GET',
                    url: "api/Project/List",
                    data: {
                        userId: self.currentUser().Id
                    },
                    success: function (data) {
                        self.projects(data);
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }

            self.load();
        };

        return { viewModel: projectsView, template: templateString };
    });