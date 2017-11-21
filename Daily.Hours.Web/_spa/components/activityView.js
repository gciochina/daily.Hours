define(['ko', 'lib/requirejs-plugins/lib/text!components/activityView.html'],
    function (ko, templateString) {
        function activityView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.startDate = ko.observable(new Date());
            self.startDate.subscribe(function (newValue) {
                self.load();
            });

            self.endDate = ko.observable(new Date());
            self.endDate.subscribe(function (newValue) {
                self.load();
            });

            self.activities = ko.observableArray();

            self.workLogTotalHours = ko.computed(function () {
                return _.reduce(self.activities(), function (sum, workLog) { return sum + workLog.Hours; }, 0);
            }, this);

            self.load = function () {
                $.ajax({
                    method: 'GET',
                    url: "api/Activity/Report",
                    data: {
                        startDate: (self.startDate() || new Date()).toISOString(),
                        endDate: (self.endDate() || new Date()).toISOString(),
                    },
                    success: function (data) {
                        self.activities(data);
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }

            self.load();
        };

        return { viewModel: activityView, template: templateString };
    });