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
                $.ajax({
                    method: "POST",
                    url: "api/User/Logout",
                    success: function (data) {
                        self.currentUser(null);
                        self.navModel("loginView");
                    },
                    error: function (error) {
                        HandleError(error);
                    }
                })
                
            }

            //let's see if we're already logged in
            $.ajax({
                method: 'GET',
                url: "api/User/WhoAmI",
                success: function (data) {
                    if (data) {
                        self.currentUser(data);
                        self.navModel("dayView");
                    }
                },
            });
        };

        return { viewModel: topBarView, template: templateString };
    });