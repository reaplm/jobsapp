using JobsApp.Models;
using JobsApp.Views;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JobsApp.ViewModels
{
    [QueryProperty(nameof(JsonItem), nameof(JsonItem))]
    public class ItemDetailViewModel : BaseViewModel
    {
         
        
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
    }
}
