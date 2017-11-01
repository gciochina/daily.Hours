define(['ko', 'lib/requirejs-plugins/lib/text!components/topBarView.html'],
    function (ko, templateString) {
        function topBarView(params) {
            var self = this;

            self.currentUser = params.currentUser;
            self.navModel = params.navModel;

            self.doTrack = function() {
                self.navModel('dayView');
            }

            self.doActivity = function () {
                self.navModel("activityView");
            }

            self.doProject = function () {
                self.navModel("projectView");
            }

            self.doProfile = function () {
                self.navModel("profileView");
            }

            self.doTask = function () {
                self.navModel("taskView");
            }
            
            self.doPeople = function () {
                self.navModel("peopleView");
            }

            self.doSignOut = function () {
                self.currentUser(null);
                self.navModel("loginView");
            }
        };

        return { viewModel: topBarView, template: templateString };
    });