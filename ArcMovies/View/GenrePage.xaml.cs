using System;
using System.Collections.Generic;
using ArcMovies.ViewModel;
using Xamarin.Forms;

namespace ArcMovies.View
{
    public partial class GenrePage : ContentPage
    {
        public GenrePage(GenreViewModel genreViewModel)
        {
            InitializeComponent();
            this.BindingContext = genreViewModel;
        }
    }
}
