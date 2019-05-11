using System;
using System.Collections.Generic;
using System.Text;

namespace ArcMovies.ViewModel
{
    public class BaseViewModel : ObservableObject
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; SetProperty(ref _isLoading, value); }
        }

        private bool _hasError;

        public bool HasError
        {
            get { return _hasError; }
            set { _hasError = value; SetProperty(ref _hasError, value); }
        }


    }
}
