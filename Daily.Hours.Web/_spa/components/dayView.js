﻿define(['ko', 'lib/requirejs-plugins/lib/text!components/dayView.html'],
    function (ko, templateString) {
        function dayView(params) {
            var self = this;
            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.activities = ko.observableArray();
            self.filterDate = ko.observable(new Date());
            self.filterDateText = ko.computed(function () {
                return moment(self.filterDate()).format('dddd, MMMM DD YYYY');
            });

            self.activityDescription = ko.observable();

            self.showEditor = ko.observable(false);
            self.showDescriptionDialog = ko.observable(false);

            self.Id = ko.observable();
            self.Project = ko.observable().extend({ required: true });
            self.Task = ko.observable().extend({ required: true });
            self.Hours = ko.observable().extend({ required: true });
            self.Description = ko.observable();

            self.workLogTotalHours = ko.computed(function () {
                return _.reduce(self.activities(), function (sum, workLog) { return sum + workLog.Hours; }, 0);
            }, this);

            self.goPreviousDay = function () {
                self.filterDate(self.filterDate().subtract(1, "days"));
            };

            self.goNextDay = function () {
                self.filterDate(self.filterDate().add(1, "days"));
            };

            self.doShowRecordActivity = function () {
                self.Id("");
                self.Project("");
                self.Task("");
                self.Hours("");
                self.Description("");
                self.showEditor(true);
            };

            self.doEditWorkLog = function (workLog) {
                self.Id(workLog.Id);
                self.Project(workLog.Project);
                self.Task(workLog.Task);
                self.Hours(workLog.Hours);
                self.Description(workLog.Description);

                self.showEditor(true);
            }

            self.showEditor.subscribe(function (newValue) {
                if (newValue) {
                    $('#task').autocomplete("option", "appendTo", ".eventInsForm");
                    $('#project').autocomplete("option", "appendTo", ".eventInsForm");
                }
            });

            self.doRecordActivity = function () {
                self.validationErrors = ko.validation.group(this, { deep: true })
                if (self.validationErrors().length > 0) {
                    self.validationErrors.showAllMessages(true);
                    return;
                }

                var urlAction = self.Id()
                    ? "api/Activity/Update"
                    : "api/Activity/Create";

                $.ajax({
                    method: 'PUT',
                    url: urlAction,
                    data: {
                        Id: self.Id(),
                        Hours: self.Hours(),
                        TaskId: self.Task().Id,
                        Date: self.filterDate().toISOString(),
                        Description: self.Description()
                    },
                    success: function (data) {
                        self.load();
                        self.showEditor(false);
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            };

            self.doShowDescription = function (activity) {
                self.activityDescription(activity.Description);
                self.showDescriptionDialog(true);
            }

            self.doRemoveWorkLog = function (workLog) {
                $.ajax({
                    method: 'GET',
                    url: "api/Activity/Delete",
                    data: {
                        workLogId: workLog.Id
                    },
                    success: function (data) {
                        self.load();
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }

            self.doCloseActivityDescription = function (activity) {
                self.showDescriptionDialog(false);
            }

            self.load = function () {
                $.ajax({
                    method: 'GET',
                    url: "api/Activity/List",
                    data: {
                        userId: self.currentUser().Id,
                        filterDate: self.filterDate().toISOString(),
                    },
                    success: function (data) {
                        self.activities(data);
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                });
            }

            self.filterDate.subscribe(function (newValue) {
                self.load();
            });
        };

        return { viewModel: dayView, template: templateString };
    });