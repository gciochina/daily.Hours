define(['ko', 'lib/requirejs-plugins/lib/text!components/dayView.html'],
    function (ko, templateString) {
        function dayView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.doAddActivity = function () {
                //show popup for adding activity
            }

            self.activities = ko.observableArray();

            $.ajax({
                method: 'GET',
                url: "api/Activity/List",
                data: {
                    userId: self.currentUser().Id(),
                    filderDate: self.filterDate(),
                },
                success: function (data) {
                    self.activities(data);
                },
                error: function (error) {
                    toastr.error(error.responseJSON.ExceptionMessage, error.responseJSON.Message);
                }
            });
        };

        return { viewModel: dayView, template: templateString };
    });