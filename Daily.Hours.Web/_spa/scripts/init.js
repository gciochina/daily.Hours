(function () {
    requirejs.config({
        baseUrl: './_spa',
        waitSeconds: 0,
        urlArgs: '1.0.0.0',
        paths: {}
    });

    define('ko', [], function () { return ko; });
    define('knockout', [], function (ko) { return ko; });
    define('toastr', function () { return toastr; });

    require(['ko'], function (ko) {
        ko.components.register('loginView', { require: 'components/loginView' });
        ko.components.register('signUpView', { require: 'components/signUpView' });
        ko.components.register('welcomeView', { require: 'components/welcomeView' });
        ko.components.register('mainView', { require: 'components/mainView' });
        ko.components.register('topBarView', { require: 'components/topBarView' });
    });

    require(['knockout'],
		function () {
            function mainView() {
                this.currentUser = ko.observable();
                this.navModel = ko.observable();
                window.CurrentUser = this.currentUser;
                window.navModel = this.navModel;
		    }
            ko.applyBindings(new mainView());
		});
})();