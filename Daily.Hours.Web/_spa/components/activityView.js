define(['ko', 'lib/requirejs-plugins/lib/text!components/activityView.html'],
    function (ko, templateString) {
        function activityView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.activities = ko.observableArray();
            self.filterDate = ko.observable(new Date());

            self.doAddActivity = function () {
                //show popup for adding activity
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
                    toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                }
            });
        };

        return { viewModel: activityView, template: templateString };
    });