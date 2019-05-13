using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcMovies.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcMovies.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public DetailPage(DetailViewModel detailViewModel)
        {
            InitializeComponent();
            this.BindingContext = detailViewModel;
        }
    }
}