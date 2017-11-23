ko.bindingHandlers.onEnter = {
    init: function (element, valueAccessor, allBindings, viewModel) {
        var callback = valueAccessor();
        $(element).keyup(function (event) {
            var keyCode = (event.which ? event.which : event.keyCode);
            if (!event.ctrlKey && keyCode === 13) {
                callback.call(viewModel);
                return false;
            }
            return true;
        });
    }
};

var formatDateTime = function (date) {
    return moment(date).format("YYYY-MM-DD");
}

//datetime picker
ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().dateTimePickerOptions || { format: 'L'};
        $(element).datetimepicker(options);

        //when a user changes the date, update the view model
        ko.utils.registerEventHandler(element, "dp.change", function (event) {
            var value = valueAccessor();
            if (ko.isObservable(value)) {
                if (event.date != null && !(event.date instanceof Date)) {
                    if (event.date === false) {
                        value(null);
                    }
                    else {
                        value(formatDateTime(event.date.toDate()));
                    }
                } else {
                    value(event.date);
                }
            }
        });

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            var picker = $(element).data("DateTimePicker");
            if (picker) {
                picker.destroy();
            }
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {

        var picker = $(element).data("DateTimePicker");
        //when the view model is updated, update the widget
        if (picker) {
            var koDate = moment(ko.utils.unwrapObservable(valueAccessor()));

            //in case return from server datetime i am get in this form for example /Date(93989393)/ then fomat this
            koDate = (typeof (koDate) !== 'object') ? new Date(parseFloat(koDate.replace(/[^0-9]/g, ''))) : koDate;

            //check if there is actually a date
            koDate = koDate < moment(0) ? moment() : koDate;

            picker.date(koDate);
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

ko.bindingHandlers.autoComplete = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var selectedItem = valueAccessor();
        if (selectedItem().Id) {
            $(element).val(selectedItem().Id);
            $(element).siblings('.ui-combobox').find('.ui-autocomplete-input').val(selectedItem().Name);
        }
        else {
            $(element).val('');
            $(element).siblings('.ui-combobox').find('.ui-autocomplete-input').val('');
        }

    },
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var getUrl = allBindingsAccessor().getUrl;
        var insertUrl = allBindingsAccessor().insertUrl;
        var canInsert = allBindingsAccessor().canInsert;
        var selectedItem = valueAccessor();

        var insertButtonId = "insertEntity_" + $(element).attr("id");
        var keyHintTextId = "inserEntity_keyHint_" + $(element).attr("id");

        $(element).autocomplete({
            minLength: 0,
            autoFocus: true,
            source: function (request, response) {
                $.ajax({
                    url: ko.isObservable(getUrl) ? getUrl() : getUrl,
                    data: {
                        term: request.term
                    },
                    dataType: "json",
                    type: "GET",
                    success: function (data) {
                        var self = this;

                        response(data.map(function (t) {
                            return {
                                label: t.Name,
                                value: t.Name,
                                Name: t.Name,
                                Id: t.Id,
                            }
                        }));

                        if (!canInsert)
                            return;

                        self.insertUrl = insertUrl;
                        if (data.length !== 0 || $(element).val() === "") {
                            $(element).addClass("col-md-12");
                            $(element).removeClass("col-md-11");
                            $("#" + insertButtonId).remove();
                            $('#' + keyHintTextId).remove();
                        }
                        else {
                            if (!$("#" + insertButtonId).length) {
                                $(element).removeClass("col-md-12");
                                $(element).addClass("col-md-11");
                                $(element).after("<button class='col-md-1' id='" + insertButtonId + "' style='float:right'>+</button>");
                                $(element).parent().after("<div id='" + keyHintTextId + "'><br/><div>Ctrl+ENTER to add</div><div>");

                                $("#" + insertButtonId).click(function () {
                                    $.ajax({
                                        url: ko.isObservable(insertUrl) ? insertUrl() : insertUrl,
                                        data: {
                                            Name: $(element).val(),
                                        },
                                        dataType: "json",
                                        type: "GET",
                                        success: function (serverInsertResponse) {
                                            $(element).autocomplete("search");
                                        }
                                    });
                                });
                            }
                        }
                    }
                });
            },
            select: function (event, ui) {
                selectedItem(ui.item);
            }
        });
        $(element).keydown(function (e) {
            if (e.ctrlKey && e.keyCode == 13 && $("#" + insertButtonId).length) {
                $("#" + insertButtonId).click();
            }
            if (e.ctrlKey && e.keyCode == 32) {
                $(this).autocomplete('search');
            }
        });
    }
};