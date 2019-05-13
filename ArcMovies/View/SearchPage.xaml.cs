using ArcMovies.ViewModel;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ArcMovies.View
{
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            this.BindingContext = new SearchViewModel();

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchText(e.NewTextValue);
        }

        void SearchText(string text)
        {
            if (this.BindingContext is SearchViewModel context)
            {
                context.SearchText = string.IsNullOrWhiteSpace(text) ? string.Empty : text;
            }
        }

        private void LstSectionName_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                if (this.BindingContext is SearchViewModel context)
                {
                    if(context.SearchByGenreCommand != null && context.SearchByGenreCommand.CanExecute(e.SelectedItem))
                    {
                        context.SearchByGenreCommand.Execute(e.SelectedItem);
                    }
                }
                ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            }
        }
    }
}
