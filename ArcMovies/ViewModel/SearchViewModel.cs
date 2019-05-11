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
        ICommand _LoadCommand;
        public ICommand LoadCommand
        {
            get { return _LoadCommand ?? (_LoadCommand = new Command(LoadGenre)); }            
        }

        private ICommand _SearchByGenreCommand;
        public ICommand SearchByGenreCommand
        {
            get { return _SearchByGenreCommand ?? (_SearchByGenreCommand = new Command(SearchByGenre)); }            
        }

        private ICommand _SearchByKeyWordCommand;
        public ICommand SearchByKeyWordCommand
        {
            get { return _SearchByKeyWordCommand ?? (_SearchByKeyWordCommand = new Command<string>(SearchByKeyWord)); }            
        }        

        ObservableCollection<Section> _sectionList;
        public ObservableCollection<Section> SectionList
        {
            get { return _sectionList; }
            set { _sectionList = value; SetProperty(ref _sectionList, value); }
        }

        ObservableCollection<Movie> _mediaList;
        public ObservableCollection<Movie> MediaList
        {
            get {return _mediaList;}
            set {_mediaList = value; SetProperty(ref _mediaList, value);}
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
