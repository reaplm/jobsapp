using JobsApp.Models;
using JobsApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JobsApp.ViewModels
{
    public class FavouritesViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Favourites { get; set; }

        private const string favouritesKey = "Likes";

        public Command LoadItemsCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command<Item> LikeTapped { get; }

        public FavouritesViewModel()
        {
            Title = "Favourites";

            Favourites = new ObservableCollection<Item>();
            Favourites.CollectionChanged += Favourites_CollectionChanged;

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);
            LikeTapped = new Command<Item>(OnLikeTapped);
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        #region Commands

        /// <summary>
        /// Load Favourites collection
        /// </summary>
        /// <returns></returns>
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                //Load from preferences
                var jsonObject = await Task.Run( () => Preferences.Get(favouritesKey,string.Empty));

                if(!string.IsNullOrEmpty(jsonObject))
                {
                    Favourites.Clear();
                    var items = JsonConvert.DeserializeObject<ObservableCollection<Item>>(jsonObject);

                    foreach (var item in items)
                        Favourites.Add(item);
                }
           
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        /// <summary>
        /// Like tapped event
        /// </summary>
        /// <param name="item">Liked item</param>
        private void OnLikeTapped(Item item)
        {
            item.Liked = !item.Liked;
            Favourites.Remove(item);
        }
        #endregion

        #region Favourites
        /// <summary>
        /// Collection changed event handler for favourites collection
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void Favourites_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SaveFavourites();
        }
        /// <summary>
        /// Commit collection to preferences after changes
        /// </summary>
        private void SaveFavourites()
        {
                Preferences.Set(favouritesKey, JsonConvert.SerializeObject(Favourites));
        }

        #endregion
    }
}
