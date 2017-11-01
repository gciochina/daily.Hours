define(['ko', 'lib/requirejs-plugins/lib/text!components/resultsViewModel.html'],
    function (ko, templateString) {
        function resultsViewModel(params) {
            var self = this;
            self.movies = params.movies;
            self.movie = params.movie;
            self.navModel = params.navModel;

            self.doDetails = function (movie) {
                self.movie(movie);
                self.navModel('detailsViewModel');
            }
        };

        return { viewModel: resultsViewModel, template: templateString };
    });