using System;
using System.Threading.Tasks;
using ArcMovies.ViewModel;

namespace ArcMovies.Navigation.Abstraction
{
    public interface INavigationPage
    {
        Task NavigateToMovieDetailAsync(DetailViewModel detailViewModel);
        Task NavigateToHomeAsync();
        Task NavigateToGenreAsync(GenreViewModel genreViewModel);
        Task NavigateToGenreAsync();
    }
}
