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

            NavigationPage homePage = new NavigationPage(new HomePage());
            NavigationPage searchPage = new NavigationPage(new SearchPage());

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
