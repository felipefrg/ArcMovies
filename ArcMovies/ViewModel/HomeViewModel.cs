using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ArcMovies.Model;
using ArcMovies.Navigation.Abstraction;
using ArcMovies.Service.Abstraction;
using ArcMovies.Service.Implementation;
using Xamarin.Forms;

namespace ArcMovies.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        #region Binding Properties
        ObservableCollection<Movie> _bannerList;
        public ObservableCollection<Movie> BannerList
        {
            get { return _bannerList; }
            set
            {
                SetProperty(ref _bannerList, value);
            }
        }

        ObservableCollection<Movie> _upcomingList;
        public ObservableCollection<Movie> UpcomingList
        {
            get { return _upcomingList; }
            set
            {
                SetProperty(ref _upcomingList, value);
            }
        }

        ObservableCollection<Movie> _topRatedList;
        public ObservableCollection<Movie> TopRatedList
        {
            get { return _topRatedList; }
            set
            {
                SetProperty(ref _topRatedList, value);
                }
        }
        ObservableCollection<Movie> _popularList;
        public ObservableCollection<Movie> PopularList
        {
            get { return _popularList; }
            set
            {
                SetProperty(ref _popularList, value);
            }
        }
        #endregion Binding Properties

        private Task LoadLists()
        {
            return Task.Run(() =>
            {
                LoadUpcoming();
                LoadPopular();
                LoadTopRated();
                                        
            });
        }

               
        private async Task LoadUpcoming()
        {
            var serviceResult = await DependencyService.Get<ITMDbService>().GetSection(new TMDbConfigMovie()            
                                                                                                            .ActionUpcoming()
                                                                                                            .WithVideos()
                                                                                                            .WithCredits());
            if (serviceResult != null && serviceResult.results != null)
            {
                this.UpcomingList = new ObservableCollection<Movie>(serviceResult.results);


                this.BannerList = new ObservableCollection<Movie>(serviceResult.results.Take(5));
            }
        }

        private async Task LoadTopRated()
        {
            var serviceResult = await DependencyService.Get<ITMDbService>().GetSection(new TMDbConfigMovie()
                                                                                                            .ActionTopRated()
                                                                                                            .WithVideos()
                                                                                                            .WithCredits());
            if (serviceResult != null && serviceResult.results != null)
            {
                this.TopRatedList = new ObservableCollection<Movie>(serviceResult.results);

            }
        }

        private async Task LoadPopular()
        {
            var serviceResult = await DependencyService.Get<ITMDbService>().GetSection(new TMDbConfigMovie()
                                                                                                            .ActionPopular()
                                                                                                            .WithVideos()
                                                                                                            .WithCredits());
            if (serviceResult != null && serviceResult.results != null)
            {
                this.PopularList = new ObservableCollection<Movie>(serviceResult.results);

            }
        }

        ICommand _selectedMovieCommand;
        public ICommand SelectedMovieCommand
        {
            get { return _selectedMovieCommand ?? (_selectedMovieCommand = 
                    new Command(async (obj) => { await GoToMovieDetail(obj); })); }
        }

        private async Task GoToMovieDetail(object obj)
        {   
            if(obj is Movie movie)
            {

                    DetailViewModel detailViewModel = new DetailViewModel(movie);
                    await DependencyService.Get<INavigationPage>().NavigateToMovieDetailAsync(detailViewModel);

            }
        }

        public HomeViewModel()
        {
            this._popularList = new ObservableCollection<Movie>();
            this._topRatedList = new ObservableCollection<Movie>();
            this._upcomingList = new ObservableCollection<Movie>();
            this._bannerList = new ObservableCollection<Movie>();

            this.Load(LoadLists);
        }
    }
}
