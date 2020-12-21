using JobsApp.Services;
using JobsApp.ViewModels;
using JobsApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace JobsApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public IAppManager AppManager => DependencyService.Get<IAppManager>();
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));

            //Menu Items
            if (AppManager.IsLoggedIn())
                FlyoutMenuItem.Text = "Logout";
            else
                FlyoutMenuItem.Text = "LogIn";
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            if (AppManager.IsLoggedIn())
            {
                //Logout
                AppManager.SignOut();
            }
            else
            {
                //Login
                await Shell.Current.Navigation.PushModalAsync(new LoginPage());
            }   

            
        }
       
    }
}
