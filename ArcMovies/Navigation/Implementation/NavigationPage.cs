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

        public Task NavigateToGenreAsync()
        {
            throw new NotImplementedException();
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
            var tabPage = Application.Current.MainPage as TabbedPage;
            if (tabPage.SelectedItem is Page _page)
            {
                await _page.Navigation.PushAsync(page);
            }
        }
    }
}
