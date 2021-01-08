using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using JobsApp.Droid.Services;
using JobsApp.Models;
using JobsApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(AppPreferences_Android))]
namespace JobsApp.Droid.Services
{
    
    public class AppPreferences_Android : IAppPreferences
    {
        private ISharedPreferences sharedPreferences;
        private ISharedPreferencesEditor editor;

        public AppPreferences_Android(ISharedPreferences sharedPreferences)
        {
            this.sharedPreferences = sharedPreferences;
            editor = sharedPreferences.Edit();

        }
        /// <summary>
        /// Save liked item in shared preferences
        /// </summary>
        /// <param name="item">Item to save</param>
        public void SaveLike(Item item)
        {
            try
            {
                var jsonObject = sharedPreferences.GetString("Likes", null);
                List<Item> likes = null;

                if (jsonObject == null)
                    likes = new List<Item>();
                else
                    likes = JsonConvert.DeserializeObject<List<Item>>(jsonObject);

                //If deserialization returns null
                if (likes == null)
                    likes = new List<Item>();

                // add your object to list of jobs
                if (!likes.Exists(x => x.Id == item.Id))
                    likes.Add(item);

                // convert the list to json
                var likesAsJson = JsonConvert.SerializeObject(likes);
                editor.PutString("Likes", likesAsJson);
                editor.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to add like to ppreferences: " + ex);
            }

        }
        /// <summary>
        /// Remove liked item from shared preferences
        /// </summary>
        /// <param name="item">Item to remove</param>
        public void DeleteLike(Item item)
        {
            try
            {
                var jsonObject = sharedPreferences.GetString("Likes", null);
                List<Item> likes = null;

                if (jsonObject == null)
                    likes = new List<Item>();
                else
                    likes = JsonConvert.DeserializeObject<List<Item>>(jsonObject);

                //If deserialization returns null
                if (likes == null)
                    likes = new List<Item>();

                // remove item from list of jobs
                var itemToRemove = likes.Find(x => x.Id == item.Id);
                if (likes.Exists(x => x.Id == item.Id))
                    likes.Remove(itemToRemove);

                // convert the list to json
                var likesAsJson = JsonConvert.SerializeObject(likes);
                editor.PutString("Likes", likesAsJson);
                editor.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to delete like to preferences: " + ex);
            }

        }
        /// <summary>
        /// Fetch all likes from preferences
        /// </summary>
        /// <returns>List of likes</returns>
        public List<Item> GetLikes()
        {
            List<Item> likes = null;

            try
            {
                var jsonObject = sharedPreferences.GetString("Likes", null);

                if (jsonObject != null)
                    likes = JsonConvert.DeserializeObject<List<Item>>(jsonObject);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to get likes from preferences: " + ex);
            }
            return likes;
        }
        /// <summary>
        /// Checked if job is liked or not
        /// </summary>
        /// <param name="item">true/false</param>
        /// <returns></returns>
        public bool IsLiked(Item item)
        {
            List<Item> likes = GetLikes();

            if (likes != null)
                if (likes.Exists(x => x.Id == item.Id))
                    return true;
            
            return false;
        }
    }
}