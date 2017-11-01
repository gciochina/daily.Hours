define(['ko', 'lib/requirejs-plugins/lib/text!components/detailsViewModel.html'],
    function (ko, templateString) {
        function detailsViewModel(params) {
            var self = this;
            self.movie = params.movie;
            self.movieDetails = ko.observable();

            $.ajax({
                method: 'get',
                url: "https://www.omdbapi.com/?i=" + self.movie().imdbID + "&y=&plot=short&r=json",
                success: function (data) {
                    self.movieDetails(data);
                },
                fail: function (error) {
                    alert(error);
                }
            });
        };

        return { viewModel: detailsViewModel, template: templateString };
    });