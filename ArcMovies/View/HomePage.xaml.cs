using ArcMovies.ViewModel;
using Xamarin.Forms;

namespace ArcMovies.View
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = new HomeViewModel();
        }
    }
}
