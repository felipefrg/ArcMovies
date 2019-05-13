using System;
using ArcMovies.Model;

namespace ArcMovies.ViewModel
{
    public class DetailViewModel : BaseViewModel
    {
        Movie _movie;
        public Movie Movie
        {
            get { return _movie; }
            private set{ SetProperty(ref _movie, value);}
        }

        public DetailViewModel(Movie movie)
        {
            this._movie = movie;
        }

    }
}
