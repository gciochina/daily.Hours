define(['ko', 'lib/requirejs-plugins/lib/text!components/footerView.html'],
    function (ko, templateString) {
        function footerView(params) {
            var self = this;

            self.currentUser = params.currentUser;
        };

        return { viewModel: footerView, template: templateString };
    });