﻿(function () {
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

    ko.bindingHandlers.datepicker = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            //initialize datepicker with some optional options
            var options = {
                format: 'DD/MM/YYYY',
                defaultDate: valueAccessor()()
            };

            if (allBindingsAccessor() !== undefined) {
                if (allBindingsAccessor().datepickerOptions !== undefined) {
                    options.format = allBindingsAccessor().datepickerOptions.format !== undefined ? allBindingsAccessor().datepickerOptions.format : options.format;
                }
            }

            $(element).datetimepicker(options);

            //when a user changes the date, update the view model
            ko.utils.registerEventHandler(element, "dp.change", function (event) {
                var value = valueAccessor();
                if (ko.isObservable(value)) {
                    value(event.date);
                }
            });

            var defaultVal = $(element).val();
            var value = valueAccessor();
            value(moment(defaultVal, options.format));
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var thisFormat = 'DD/MM/YYYY HH:mm';

            if (allBindingsAccessor() !== undefined) {
                if (allBindingsAccessor().datepickerOptions !== undefined) {
                    thisFormat = allBindingsAccessor().datepickerOptions.format !== undefined ? allBindingsAccessor().datepickerOptions.format : thisFormat;
                }
            }

            var value = valueAccessor();
            var unwrapped = ko.utils.unwrapObservable(value());

            if (unwrapped === undefined || unwrapped === null) {
                element.value = new moment(new Date());
                console.log("undefined");
            } else {
                element.value = unwrapped.format(thisFormat);
            }
        }
    };

    ko.bindingHandlers.dialog = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var options = ko.utils.unwrapObservable(valueAccessor()) || {};
            //do in a setTimeout, so the applyBindings doesn't bind twice from element being copied and moved to bottom
            setTimeout(function () {
                options.close = function () {
                    allBindingsAccessor().dialogVisible(false);
                };

                $(element).dialog(options);
            }, 0);

            //handle disposal (not strictly necessary in this scenario)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).dialog("destroy");
            });
        },
        update: function (element, valueAccessor, allBindingsAccessor) {
            var shouldBeOpen = ko.utils.unwrapObservable(allBindingsAccessor().dialogVisible),
                $el = $(element),
                dialog = $el.data("uiDialog") || $el.data("dialog");

            //don't call open/close before initilization
            if (dialog) {
                $el.dialog(shouldBeOpen ? "open" : "close");
            }
        }
    };

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