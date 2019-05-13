using System;
using ArcMovies.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcMovies
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            TabbedPage tabbedPage = new TabbedPage();

            tabbedPage.BarBackgroundColor = Color.Black;

            NavigationPage homePage = new NavigationPage(new HomePage());
            homePage.Icon = new FileImageSource().File = "home_gray.png";
            homePage.Title = "Home";

            NavigationPage searchPage = new NavigationPage(new SearchPage());
            searchPage.Icon = new FileImageSource().File = "search_gray.png";
            searchPage.Title = "Search";

            homePage.BarBackgroundColor = Color.Black;
            homePage.BarTextColor = Color.Gray;

            searchPage.BarBackgroundColor = Color.Black;
            searchPage.BarTextColor = Color.Gray;

            tabbedPage.Children.Add(homePage);
            tabbedPage.Children.Add(searchPage);

            NavigationPage.SetHasNavigationBar(tabbedPage, false);



            MainPage = new NavigationPage(tabbedPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
