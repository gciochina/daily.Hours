define(['ko', 'lib/requirejs-plugins/lib/text!components/dayView.html'],
    function (ko, templateString) {
        function dayView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.activities = ko.observableArray();
            self.tasks = ko.observableArray();
            self.filterDate = ko.observable(new Date());
            self.filterDateText = ko.computed(function () {
                return moment(self.filterDate()).format('dddd, MMMM DD YYYY');
            });

            self.showAddDialog = ko.observable(false);
            self.Hours = ko.observable();
            self.Task = ko.observable();

            self.goPreviousDay = function () {
                self.filterDate(self.filterDate().subtract(1, "days"));
            }

            self.goNextDay = function () {
                self.filterDate(self.filterDate().add(1, "days"));
            }

            self.doShowRecordActivity = function () {
                $.ajax({
                    method: 'GET',
                    url: "api/Task/List",
                    success: function (data) {
                        self.showAddDialog(true);
                        self.tasks(data);
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }

            self.doRecordActivity = function () {
                $.ajax({
                    method: 'PUT',
                    url: "api/Activity/Create",
                    data: {
                        Hours: self.Hours(),
                        TaskId: self.Task().Id,
                        Date: self.filterDate().toISOString()
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

            self.load = function () {
                $.ajax({
                    method: 'GET',
                    url: "api/Activity/List",
                    data: {
                        userId: self.currentUser().Id,
                        filterDate: self.filterDate().toISOString(),
                    },
                    success: function (data) {
                        self.activities(data);
                    },
                    error: function (error) {
                        toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                    }
                });
            }

            self.filterDate.subscribe(function (newValue) {
                self.load();
            });
        };

        return { viewModel: dayView, template: templateString };
    });