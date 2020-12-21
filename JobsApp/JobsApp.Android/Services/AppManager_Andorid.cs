using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using JobsApp.Droid.Services;
using JobsApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(AppManager_Android))]
namespace JobsApp.Droid.Services
{
    public class AppManager_Android : IAppManager
    {
        FirebaseApp app;
        FirebaseAuth auth; 

        public AppManager_Android()
        {
            app = FirebaseApp.Instance;
            auth = FirebaseAuth.GetInstance(app);
        }
        /// <summary>
        /// Show Toast message
        /// </summary>
        /// <param name="message"></param>
        public void Show(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long);
        }
        /// <summary>
        /// Signout of google-services
        /// </summary>
        public void SignOut()
        {
            //
            if (auth.CurrentUser != null)
                auth.SignOut();
        }
        /// <summary>
        /// Check whether user is logged in 
        /// </summary>
        /// <returns>True/False indicating whether user is logged in or not</returns>
        public bool IsLoggedIn()
        {
            return auth.CurrentUser == null ? false : true;
        }
    }
}