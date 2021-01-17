using JobsApp.Models;
using JobsApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JobsApp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private SettingsItem _selectedItem;

        public List<SettingsItem> SettingsItems { set; get; }

        public SettingsViewModel()
        {
            Title = "Settings";
            LoadItems();

        }

        public SettingsItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        async void OnItemSelected(SettingsItem item)
        {
            if (item == null)
                return;

            switch (item.Title)
            {
                case "Premium":
                    break;
                case "Notifications":
                    break;
                case "Location":
                    break;
                case "Appearance":
                    break;
                case "Help":
                    break;
            }

        }
        //Listview ITems
        private void LoadItems()
        {
            SettingsItems = new List<SettingsItem>
            {
                new SettingsItem { Title = "Premium", SubTitle = "Upgrade to premium", Icon = "upgrade_24.png"},
                new SettingsItem { Title = "Notifications", SubTitle = "Push notifications", Icon = "notification_24.png"},
                new SettingsItem { Title = "Location", SubTitle = "Change location settings", Icon = "location_24.png"},
                new SettingsItem { Title = "Appearance", SubTitle = "Theme, font size", Icon = "palette_24.png"},
                new SettingsItem { Title = "Help", SubTitle = "FAQ, feedback, contact us", Icon = "help_24.png"}
            };
        }
    }
}
