define(['ko', 'lib/requirejs-plugins/lib/text!components/tasksView.html'],
    function (ko, templateString) {
        function tasksView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.tasks = ko.observableArray();

            self.doAddTask = function () {

            }

            self.deleteTask = function (taskId) {
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