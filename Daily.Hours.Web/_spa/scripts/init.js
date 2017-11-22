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
    define('jquery', function () { return jQuery; });

    require(['ko'], function (ko) {
        ko.components.register('loginView', { require: 'components/loginView' });
        ko.components.register('signUpView', { require: 'components/signUpView' });
        ko.components.register('welcomeView', { require: 'components/welcomeView' });
        ko.components.register('topBarView', { require: 'components/topBarView' });
        ko.components.register('footerView', { require: 'components/footerView' });
        ko.components.register('profileView', { require: 'components/profileView' });

        ko.components.register('mainView', { require: 'components/mainView' });
        ko.components.register('dayView', { require: 'components/dayView' });

        ko.components.register('activityView', { require: 'components/activityView' });
        ko.components.register('projectsView', { require: 'components/projectsView' });
        ko.components.register('tasksView', { require: 'components/tasksView' });
        ko.components.register('peopleView', { require: 'components/peopleView' });
    });

    ko.validation.rules.pattern.message = 'Invalid.';

    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });

    require(['knockout'],
		function () {
            function mainAppView() {
                this.currentUser = ko.observable();
                this.navModel = ko.observable();
                window.currentUser = this.currentUser;
                window.navModel = this.navModel;
		    }
            ko.applyBindings(new mainAppView());
		});
})();

HandleError = function (error) {
    toastr.error((error.responseJSON || { ExceptionMessage: "Something went wrong. Thats all we know" }).ExceptionMessage,
        (error.responseJSON || { Message: "Whoops" }).Message);
}