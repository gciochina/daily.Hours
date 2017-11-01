define(['ko', 'lib/requirejs-plugins/lib/text!components/mainView.html'],
    function (ko, templateString) {
        function mainView(params) {
            var self = this;
            self.currentUser = params.currentUser;

            self.navModel = self.currentUser()
                ? ko.observable(navModel)
                : ko.observable("loginView");
        };

        return { viewModel: mainView, template: templateString };
    });