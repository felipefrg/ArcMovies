using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ArcMovies.Model;
using ArcMovies.Navigation.Abstraction;
using ArcMovies.Service.Abstraction;
using ArcMovies.Service.Implementation;
using Xamarin.Forms;

namespace ArcMovies.ViewModel
{
    public class GenreViewModel :BaseViewModel
    {
        Genre _genre;
        TMDbConfigDiscover _config;

        public GenreViewModel(Genre genre)
        {
            _genre = genre;

            _config = new TMDbConfigDiscover()
                                           .ActionMovie()
                                           .WithGenres(_genre.id)
                                           .WithVideos()
                                           .WithCredits();

            this._movieList = new ObservableCollection<Movie>();
            this.Load(LoadSection);
        }

        Section _section;
        public Section Section2
        {
            get { return _section; }
            set { SetProperty(ref _section, value); }
        }

        ObservableCollection<Movie> _movieList;
        public ObservableCollection<Movie> MovieList
        {
            get { return _movieList; }
            set { SetProperty(ref _movieList, value); }
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
        private async Task LoadSection()
        {
            var serviceResult = await DependencyService.Get<ITMDbService>()
                                                       .GetDiscover(_config);
            if(serviceResult != null && serviceResult.results != null)
            {
                this.MovieList = new ObservableCollection<Movie>(serviceResult.results);
                this.TotalPage = serviceResult.total_pages;
                this.TotalResults = serviceResult.total_results;
                this.CurrentPage = serviceResult.page;

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

    }
}
