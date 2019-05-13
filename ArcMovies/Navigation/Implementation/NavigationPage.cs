using System;
using System.Threading.Tasks;
using ArcMovies.Navigation.Abstraction;
using ArcMovies.View;
using ArcMovies.ViewModel;
using Xamarin.Forms;

[assembly: Dependency(typeof(ArcMovies.Navigation.Implementation.NavigationPage))]
namespace ArcMovies.Navigation.Implementation
{
    public class NavigationPage : INavigationPage
    {
        public async Task NavigateToGenreAsync(GenreViewModel genreViewModel)
        {
            GenrePage genrePage = new GenrePage(genreViewModel);
            await NavigateTo(genrePage);
        }      

        public Task NavigateToHomeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task NavigateToMovieDetailAsync(DetailViewModel detailViewModel)
        {
            DetailPage page = new DetailPage(detailViewModel);
            await NavigateTo(page);
        }

        private async Task NavigateTo(Page page)
        {
            if (Application.Current.MainPage is Xamarin.Forms.NavigationPage navigationPage)
            {
                if (navigationPage.CurrentPage is TabbedPage tabPage)
                {
                    if (tabPage.CurrentPage is Page _page)
                    {
                        await _page.Navigation.PushAsync(page);
                    }
                }
            }
        }
    }
}
