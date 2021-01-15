using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Google.Cloud.Firestore;

namespace JobsApp.Models
{
    [FirestoreData]
    public class Item : INotifyPropertyChanged, IEquatable<Item>
    {
        [FirestoreDocumentId]
        public string Id { set; get; }
        [FirestoreProperty]
        public string Title { set; get; }
        [FirestoreProperty]
        public string Description { set; get; }
        [FirestoreProperty]
        public string Source { set; get; }
        [FirestoreProperty]
        public string Type { set; get; }
        [FirestoreProperty]
        public string Location { set; get; }
        [FirestoreProperty]
        public string Company { set; get; }
        [FirestoreProperty]
        public DateTime Closing { set; get; }
        [FirestoreProperty]
        public DateTime Posted { set; get; }
        [FirestoreProperty]
        public List<string> Contacts { set; get; }
        [FirestoreProperty]
        public List<string> Competencies { set; get; }
        [FirestoreProperty]
        public List<string> Qualifications { set; get; }

        private bool liked = false;
        public bool Liked
        {
            set {
                SetProperty(ref liked, value);
                OnPropertyChanged(nameof(LikeSource));
                
            }

            get { return liked; }
        }
        public string LikeSource 
        {
            get 
            { 
                return Liked ? "heart_filled" : "heart_outline"; 
            }
        } 

        public string PostedDateString
        {
            get
            {
                return Posted.ToShortDateString();
            }
        }
        public string ClosingDateString
        {
            get
            {
                return "Closes " + Closing.ToShortDateString();
            }
        }
        public string Abrev 
        { 
            get
            {
                return Title.Substring(0, 1);
            } 
        }
        public string QualificationsToString
        {
            get
            {
                var qstring = "";
                if (Qualifications != null)
                {
                    foreach (var q in Qualifications)
                    {
                        qstring += "• " + q + "\n";
                    }
                }
                return qstring;
            }
        }
        public string CompetenciesToString
        {
            get
            {
                var cstring = "";
                if (Competencies != null)
                {
                    foreach (var c in Competencies)
                    {
                        cstring += "• " + c + "\n";
                    }
                }
                return cstring;
            }
        }
        public string ContactsToString
        {
            get
            {
                var cstring = "";
                if (Contacts != null)
                {
                    foreach (var c in Contacts)
                    {
                        cstring += c + "\n";
                    }
                }
                return cstring;
            }
        }
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Equals(Item other)
        {
            return this.Id == other.Id;
        }
        #endregion
    }
}