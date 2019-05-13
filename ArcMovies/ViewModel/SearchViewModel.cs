using ArcMovies.Model;
using ArcMovies.Service.Abstraction;
using ArcMovies.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArcMovies.ViewModel
{
    public class SearchViewModel : BaseViewModel
    {
        ICommand _loadCommand;
        public ICommand LoadCommand
        {
            get { return _loadCommand ?? (_loadCommand = new Command(async () => { await LoadAllGenres(); })); }
        }

        private ICommand _searchByGenreCommand;
        public ICommand SearchByGenreCommand
        {
            get { return _searchByGenreCommand ?? (_searchByGenreCommand = new Command(SearchByGenre)); }
        }

        private ICommand _searchByKeyWordCommand;
        public ICommand SearchByKeyWordCommand
        {
            get { return _searchByKeyWordCommand ?? (_searchByKeyWordCommand = new Command<string>(SearchByKeyWord)); }
        }

        ObservableCollection<Genre> _genreList;
        public ObservableCollection<Genre> GenreList
        {
            get { return _genreList; }
            set { SetProperty(ref _genreList, value); }
        }

        ObservableCollection<Movie> _movieList;
        public ObservableCollection<Movie> MovieList
        {
            get { return _movieList; }
            set { SetProperty(ref _movieList, value); }
        }

        async Task SearchByGenre(object obj)
        {
            if (obj is Genre genre)
            {
                var serviceResult = await DependencyService.Get<ITMDbService>().GetDiscover(new TMDbConfigDiscover()
                                                                                             .ActionMovie()
                                                                                             .WithGenres(genre.id)
                                                                                             .WithVideos()
                                                                                             .WithCredits());
                if (serviceResult != null && serviceResult.results != null)
                {
                    this.MovieList = new ObservableCollection<Movie>(serviceResult.results);
                }
            }
        }

        private void SearchByKeyWord(string keyword)
        {

        }

        private async Task LoadAllGenres()
        {
            var serviceResult = await DependencyService.Get<ITMDbService>().GetGenres(new TMDbConfigGenre());
            if (serviceResult != null && serviceResult.genres != null)
            {
                this.GenreList = new ObservableCollection<Genre>(serviceResult.genres);
            }
        }

        public SearchViewModel()
        {
            this._genreList = new ObservableCollection<Genre>();
            this._movieList = new ObservableCollection<Movie>();

            this.Load(LoadAllGenres);
        }
    }
}
