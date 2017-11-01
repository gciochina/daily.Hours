define(['ko', 'lib/requirejs-plugins/lib/text!components/welcomeView.html'],
    function (ko, templateString) {
        function welcomeView(params) {
            var self = this;

            self.navModel = params.navModel;
            self.currentUser = params.currentUser;
        };

        return { viewModel: welcomeView, template: templateString };
    });