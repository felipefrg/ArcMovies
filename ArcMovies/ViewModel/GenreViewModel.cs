using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ArcMovies.Model;
using ArcMovies.Service.Abstraction;
using ArcMovies.Service.Implementation;
using Xamarin.Forms;

namespace ArcMovies.ViewModel
{
    public class GenreViewModel :BaseViewModel
    {
        Genre _genre;
        public GenreViewModel(Genre genre)
        {
            _genre = genre;
        }

        ObservableCollection<Movie> movieList;
        public ObservableCollection<Movie> MovieList
        {
            get { return movieList; }
            set { SetProperty(ref movieList, value); }
        }

        private async Task LoadMovies()
        {
            var serviceResult = await DependencyService.Get<ITMDbService>()
                                                        .GetDiscover(new TMDbConfigDiscover()
                                                                                            .ActionMovie()
                                                                                            .WithGenres(_genre.id)
                                                                                            .WithVideos()
                                                                                            .WithCredits());
            if (serviceResult != null && serviceResult.results != null)
            {
                this.MovieList = new ObservableCollection<Movie>(serviceResult.results);
            }
        }
    }
}
