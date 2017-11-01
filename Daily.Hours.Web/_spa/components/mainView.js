define(['ko', 'lib/requirejs-plugins/lib/text!components/mainView.html'],
    function (ko, templateString) {
        function mainView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = ko.observable("dayView");
        };

        return { viewModel: mainView, template: templateString };
    });