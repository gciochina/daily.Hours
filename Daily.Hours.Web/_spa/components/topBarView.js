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
                self.navModel("projectsView");
            }

            self.doProfile = function () {
                self.navModel("profileView");
            }

            self.doTask = function () {
                self.navModel("tasksView");
            }
            
            self.doPeople = function () {
                self.navModel("peopleView");
            }

            self.doSignOut = function () {
                self.currentUser(null);
                $.ajax({
                    method: "POST",
                    url: "api/User/Logout",
                    success: function () {
                        self.navModel("loginView");
                    },
                    error: function (error) {
                        HandleError(error);
                    },
                })
                
            }
        };

        return { viewModel: topBarView, template: templateString };
    });