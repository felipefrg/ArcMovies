using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcMovies.ViewModel
{
    public abstract class BaseViewModel : ObservableObject
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

        protected virtual async Task Load(Func<Task> func)
        {
            try
            {
                IsLoading = true;
                HasError = false;
                await func.Invoke();
            }
            catch
            {
                HasError = true;
            }
            finally
            {
                IsLoading = false;
            }
        }


    }
}
