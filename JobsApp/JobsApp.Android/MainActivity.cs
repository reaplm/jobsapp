using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using JobsApp.Services;
using JobsApp.Droid.Services;
using System.IO;

namespace JobsApp.Droid
{
    [Activity(Label = "JobsApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static string fileName = "jobsapp-c8100-7dd3e3e004b2.json";
        private static string filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CreateCreadentialsFile();

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public string GetJsonFilePath()
        {
            return Path.Combine(filePath, fileName);
        }
        /// <summary>
        /// Create GoogleCredential json file for connecting to Firestore
        /// </summary>
        /// <returns>path to a json file containing credential</returns>
        public void CreateCreadentialsFile()
        {
            string path = GetJsonFilePath();

            try
            {
                if (!File.Exists(path))
                {
                    using (BinaryReader br = new BinaryReader(Android.App.Application.Context.Assets.Open(fileName)))
                    {
                        using (BinaryWriter bw = new BinaryWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
                        {
                            int length = 2048;
                            int bytesRead = 0;
                            byte[] buffer = new byte[length];


                            while ((bytesRead = br.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                bw.Write(buffer, 0, bytesRead);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Copy Credentials Exception: " + ex.Message);
            }
        }
    }
}