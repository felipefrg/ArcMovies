using ArcMovies.Model;
using ArcMovies.Navigation.Abstraction;
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
            get { return _searchByGenreCommand ?? (_searchByGenreCommand = new Command(async (genre) => {await SearchByGenre(genre); })); }
        }

        private ICommand _searchByKeyWordCommand;
        public ICommand SearchByKeyWordCommand
        {
            get { return _searchByKeyWordCommand ?? (_searchByKeyWordCommand = new Command<string>(async (key) => { await SearchByKeyWord(key); })); }
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
                GenreViewModel genreViewModel = new GenreViewModel(genre);
                await DependencyService
                    .Get<Navigation.Abstraction.INavigationPage>()
                    .NavigateToGenreAsync(genreViewModel);
            }
        }

        string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetProperty(ref _searchText, value);

                if (_searchText.Length == 0)
                {
                    this.MovieList = new ObservableCollection<Movie>();
                    this.TotalPage = 0;
                    this.TotalResults = 0;
                    this.CurrentPage = 0;
                }
            }
        }

        private async Task SearchByKeyWord(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return;

            _config.ByName(keyword);

            var serviceResult = await DependencyService.Get<ITMDbService>().GetSection(_config);
            if (serviceResult != null && serviceResult.results != null)
            {
                this.MovieList = new ObservableCollection<Movie>(serviceResult.results);
                this.TotalPage = serviceResult.total_pages;
                this.TotalResults = serviceResult.total_results;
                this.CurrentPage = serviceResult.page;
            }
        }

        private async Task LoadAllGenres()
        {
            var serviceResult = await DependencyService.Get<ITMDbService>().GetGenres(new TMDbConfigGenre());
            if (serviceResult != null && serviceResult.genres != null)
            {
                this.GenreList = new ObservableCollection<Genre>(serviceResult.genres);
            }
        }

        ICommand _selectedMovieCommand;
        public ICommand SelectedMovieCommand
        {
            get
            {
                return _selectedMovieCommand ?? (_selectedMovieCommand =
                  new Command(async (obj) => { await GoToMovieDetail(obj); }));
            }
        }
        ICommand _loadMoreMoviesCommand;
        public ICommand LoadMoreMoviesCommand
        {
            get
            {
                return _loadMoreMoviesCommand ?? (_loadMoreMoviesCommand =
                  new Command(LoadMoreMovies));
            }
        }

        bool _hasMoreItem;
        public bool HasMoreItem
        {
            get { return _hasMoreItem; }
            set
            {
                SetProperty(ref _hasMoreItem, value);
            }
        }

        int _currentPage;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                SetProperty(ref _currentPage, value);
                HasMoreItem = this._currentPage < this._totalPage;
            }
        }

        int _totalPage;
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                SetProperty(ref _totalPage, value);
            }
        }

        int _totalResults;
        public int TotalResults
        {
            get { return _totalResults; }
            set
            {
                SetProperty(ref _totalResults, value);
            }
        }

        bool _isLoadingItems;
        public bool IsLoadingItems
        {
            get { return _isLoadingItems; }
            set
            {
                SetProperty(ref _isLoadingItems, value);
            }
        }

        private async Task GoToMovieDetail(object obj)
        {
            if (obj is Movie movie)
            {
                DetailViewModel detailViewModel = new DetailViewModel(movie);
                await DependencyService.Get<INavigationPage>().NavigateToMovieDetailAsync(detailViewModel);
            }
        }

        private void LoadMoreMovies(object e)
        {
            Task.Run(async () =>
            {
                try
                {
                    if (HasMoreItem)
                    {
                        if (this.MovieList != null)
                        {
                            if (MovieList.Count == 0)
                                return;

                            this.IsLoadingItems = true;
                            int page = this.CurrentPage + 1;
                            IList<Movie> result = await GetMovies(_config.WithPage(page));

                            if (result != null)
                            {
                                foreach (var item in result)
                                {
                                    MovieList.Add(item);
                                }
                                this.CurrentPage = page;
                                MovieList = new ObservableCollection<Movie>(MovieList);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    this.IsLoadingItems = false;
                }
            });
        }

        private async Task<IList<Movie>> GetMovies(ITMDbConfig config)
        {
            IList<Movie> result = null;
            var serviceResult = await DependencyService.Get<ITMDbService>()
                                                        .GetDiscover(config);

            if (serviceResult != null && serviceResult.results != null)
            {
                result = new ObservableCollection<Movie>(serviceResult.results);
            }
            return result;
        }

        TMDbConfigSearch _config;
        public SearchViewModel()
        {
            _config = new TMDbConfigSearch()
                                           .ActionMovie()
                                           .WithVideos()
                                           .WithCredits();
            this._genreList = new ObservableCollection<Genre>();
            this._movieList = new ObservableCollection<Movie>();

            this.Load(LoadAllGenres);
        }
    }
}
