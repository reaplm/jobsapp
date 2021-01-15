using JobsApp.Models;
using JobsApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JobsApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        private const string favouritesKey = "Likes";

        public ObservableCollection<Item> Items { get; }
        public List<Item> Favourites { get; set; }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command<Item> LikeTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse Jobs";

            Items = new ObservableCollection<Item>();

            Favourites = new List<Item>();            

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);
            LikeTapped = new Command<Item>(OnLikeTapped);
            //AddItemCommand = new Command(OnAddItem);
        }
       
        #region Commands

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.FindAllAsync(true);
                foreach (var item in items)
                {
                    item.Liked = Favourites.Exists(x => x.Id == item.Id);
                    Items.Add(item);
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
        /// Like button tapped event handler
        /// </summary>
        /// <param name="item"></param>
        private void OnLikeTapped(Item item)
        {
            item.Liked = !item.Liked;

            //Save item to preferences
            if (item.Liked)
            {
                if(!Favourites.Exists(x => x.Id == item.Id))
                    Favourites.Add(item);
            }
                
            else
                Favourites.Remove(item);

            SaveFavourites();

        }
        #endregion


        /* private async void OnAddItem(object obj)
         {
             await Shell.Current.GoToAsync(nameof(NewItemPage));
         }*/
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;

            LoadFavourites();
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

            var jsonItem = Uri.EscapeDataString(JsonConvert.SerializeObject(item));

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.JsonItem)}={jsonItem}");
        }

        #region Favourites
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