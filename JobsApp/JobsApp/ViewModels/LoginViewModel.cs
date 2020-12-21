using JobsApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JobsApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        public string Email
        {
            set 
            {
                SetProperty(ref _email, value);
            }
            get => _email; 
        }
        private string _password;
        public string Password
        {
            set
            {
                SetProperty(ref _password, value);
            }
            get => _password; 
        }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }
        /// <summary>
        /// Submit login
        /// </summary>
        /// <param name="obj"></param>
        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            var token = await DataStore.SignInWithEmailAndPassword(Email, Password);

            if(token != null)
            {
                //show success msg and remove login page from stack
                AppManager.Show("Login Successful!");

                await Shell.Current.Navigation.PopModalAsync();

            }
            else
                //Dialog if failed log in
                await Application.Current.MainPage.DisplayAlert("Authentication Failed", "Email or password are incorrect.", "OK");
        }
    }
}
