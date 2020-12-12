using System;
using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace JobsApp.Models
{
    [FirestoreData]
    public class Item
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
        public bool Liked { set; get; }
        [FirestoreProperty]
        public List<string> Contacts { set; get; }
        [FirestoreProperty]
        public List<string> Competencies { set; get; }
        [FirestoreProperty]
        public List<string> Qualifications { set; get; }

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
    }
}