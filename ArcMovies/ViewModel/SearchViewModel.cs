using ArcMovies.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArcMovies.ViewModel
{
    public class SearchViewModel : BaseViewModel
    {
        ICommand _loadCommand;
        public ICommand LoadCommand
        {
            get { return _loadCommand ?? (_loadCommand = new Command(LoadGenre)); }            
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

        ObservableCollection<Section> _sectionList;
        public ObservableCollection<Section> SectionList
        {
            get { return _sectionList; }
            set { SetProperty(ref _sectionList, value); }
        }

        ObservableCollection<Movie> _mediaList;
        public ObservableCollection<Movie> MediaList
        {
            get {return _mediaList;}
            set {SetProperty(ref _mediaList, value);}
        }

        void SearchByGenre(object section)
        { }

        private void SearchByKeyWord(string keyword)
        {
            
        }

        private void LoadGenre(object genre)
        {
            
        }
    }
}
