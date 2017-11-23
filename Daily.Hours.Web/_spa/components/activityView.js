define(['ko', 'lib/requirejs-plugins/lib/text!components/activityView.html'],
    function (ko, templateString) {
        function activityView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.startDate = ko.observable(moment().format("YYYY-MM-DD"));
            self.endDate = ko.observable(moment().format("YYYY-MM-DD"));

            self.activities = ko.observableArray();

            self.workLogTotalHours = ko.computed(function () {
                return _.reduce(self.activities(), function (sum, workLog) { return sum + workLog.Hours; }, 0);
            }, this);

            self.load = function () {
                $.ajax({
                    method: 'GET',
                    url: "api/Activity/Report",
                    data: {
                        startDate: (self.startDate() || new Date()),
                        endDate: (self.endDate() || new Date()),
                    },
                    success: function (data) {
                        self.activities(data);
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }

            ko.computed(function () {
                self.startDate();
                self.endDate();
                self.load();
            });

        };

        return { viewModel: activityView, template: templateString };
    });