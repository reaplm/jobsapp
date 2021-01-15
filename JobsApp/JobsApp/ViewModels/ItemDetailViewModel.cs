using JobsApp.Models;
using JobsApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JobsApp.ViewModels
{
    [QueryProperty(nameof(JsonItem), nameof(JsonItem))]
    public class ItemDetailViewModel : BaseViewModel
    {
        public Command LikeTapped { get; }

        private const string favouritesKey = "Likes";
        public List<Item> Favourites { get; set; }

        public ItemDetailViewModel()
        {
            Favourites = new List<Item>();
            LoadFavourites();
            LikeTapped = new Command(OnLikeTapped);
        }

        private string jsonItem;
        public string JsonItem
        {
            get
            {
                return jsonItem;
            }
            set
            {
                jsonItem = Uri.UnescapeDataString(value);
                LoadItem(jsonItem);
                OnPropertyChanged("Item");
                
            }
        }
        private Item item;
        public Item Item
        {
            get
            {
                return item;
            }
            set
            {
                SetProperty(ref item, value);
                OnPropertyChanged("QualificationsHeading");
                OnPropertyChanged("CompetenciesHeading");
                OnPropertyChanged("DescriptionHeading");

            }
        }
        public string QualificationsHeading
        {
            get
            {
                if(item != null)
                {
                    return Item.Qualifications == null ? "" : "Qualifications";
                }
                else { return ""; }
            }
        }
        public string CompetenciesHeading
        {
            get
            {
                if (item != null)
                {
                    return Item.Competencies == null ? "" : "Competencies";
                }
                else { return ""; }
            }
        }
        public string DescriptionHeading
        {
            get
            {
                if (item != null)
                {

                    return Item.Description == null ? "" : "Job Description";
                }
                else { return ""; }
            }
        }
        public async void LoadItem(string item)
        {
            try
            {
                Item = await Task.Run(() => JsonConvert.DeserializeObject<Item>(item));
                Title = Item.Title;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Load Item: " + ex);
            }
        }
        #region Favourites
        /// <summary>
        /// Like button tapped event handler
        /// </summary>
        /// <param name="item"></param>
        private void OnLikeTapped()
        {
            Item.Liked = !Item.Liked;

            //Save item to preferences
            if (Item.Liked)
            {
                if (!Favourites.Exists(x => x.Id == Item.Id))
                    Favourites.Add(Item);
            }

            else
                Favourites.Remove(Item);

            SaveFavourites();

        }
        /// <summary>
        /// Load favourite items from preferences
        /// </summary>
        private void LoadFavourites()
        {
            string jsonObject = Preferences.Get(favouritesKey, null);

            if (jsonObject != null)
                Favourites = JsonConvert.DeserializeObject<List<Item>>(jsonObject);

        }
        /// <summary>
        /// Save favourite items to preferences
        /// </summary>
        private void SaveFavourites()
        {
            Preferences.Set(favouritesKey, JsonConvert.SerializeObject(Favourites));
        }
        #endregion
    }
}
