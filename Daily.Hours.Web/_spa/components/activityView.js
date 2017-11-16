define(['ko', 'lib/requirejs-plugins/lib/text!components/activityView.html'],
    function (ko, templateString) {
        function activityView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.activities = ko.observableArray();
            self.tasks = ko.observableArray();
            self.filterDate = ko.observable(new Date());

            self.showAddDialog = ko.observable(false);
            self.Hours = ko.observable();
            self.TaskId = ko.observable();

            self.doShowRecordActivity = function () {
                $.ajax({
                    method: 'GET',
                    url: "api/Task/List",
                    success: function (data) {
                        self.showAddDialog(true);
                        self.tasks(data);
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }

            self.doRecordActivity = function () {
                $.ajax({
                    method: 'PUT',
                    url: "api/Activity/Create",
                    data: {
                        Hours: self.Hours(),
                        TaskId: self.TaskId(),
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

            $.ajax({
                method: 'GET',
                url: "api/Activity/List",
                data: {
                    userId: self.currentUser().Id,
                    filterDate: (self.filterDate() || new Date()).toISOString(),
                },
                success: function (data) {
                    self.activities(data);
                },
                error: function (error) {
                    HandleError(error);
                }
            });
        };

        return { viewModel: activityView, template: templateString };
    });