using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcMovies.ViewModel
{
    public abstract class   BaseViewModel : ObservableObject
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        private bool _hasError;

        public bool HasError
        {
            get { return _hasError; }
            set { SetProperty(ref _hasError, value); }
        }

        protected virtual void Load(Func<Task> func)
        {
            Task.Run(async () => { 
            try
            {
                IsLoading = true;
                HasError = false;
                await func.Invoke();
            }
            catch(Exception ex)
            {
                HasError = true;
                Console.WriteLine($"Exception Message: {ex.Message} - StackTrace: {ex.StackTrace}");
            }
            finally
            {
                IsLoading = false;
            }
            });
        }


    }
}
