﻿using Google.Cloud.Firestore;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using JobsApp.Droid.Services;
using JobsApp.Services;
using System.Threading.Tasks;
using JobsApp.Models;
using Grpc.Core;
using Google.Apis.Auth.OAuth2;
using Grpc.Auth;
using Console = System.Console;
using System.IO;
using Firebase;

[assembly: Xamarin.Forms.Dependency(typeof(Firebase_Android))]
namespace JobsApp.Droid.Services
{
    public class Firebase_Android : IFirebase
    {
        private FirebaseApp app; 
        private FirestoreDb db;
        private const string filename = "jobsapp-c8100-7dd3e3e004b2.json";

        public Firebase_Android()
        {
            app = FirebaseApp.Instance;

            InitializeFirebase();           
        }
        /// <summary>
        /// Fetch all items from the database
        /// </summary>
        /// <param name="forceRefresh">Refresh</param>
        /// <returns>List of items</returns>
        public async Task<IEnumerable<Item>> FindAllAsync(bool forceRefresh = false)
        {
            List<Item> jobs = new List<Item>();

            try
            {

                CollectionReference collection = db.Collection("jobs");

                QuerySnapshot documents = await collection.GetSnapshotAsync();



                foreach (DocumentSnapshot document in documents)
                {
                    jobs.Add(document.ConvertTo<Item>());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in FindAllAsync: " + ex.Message);
            }

            return jobs;
        }
        /// <summary>
        /// Fetch item with given document id
        /// </summary>
        /// <param name="id">Document Id</param>
        /// <returns>The document item</returns>
        public async Task<Item> FindAsync(string id)
        {
            Item item = null;
            try
            {
                CollectionReference collection = db.Collection("jobstest");
                DocumentSnapshot document = await collection.Document(id).GetSnapshotAsync();
                item = document.ConvertTo<Item>();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return item;
        }
        /// <summary>
        /// Sign into firebase using password and email
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="password">User's password</param>
        /// <returns>User's Token Id</returns>
        public async Task<string> SignInWithEmailAndPassword(string email, string password)
        {
            string result = string.Empty;
            try
            {
                var user = await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                result = user.User.GetIdToken(false).Result.ToString();
            }
            catch (FirebaseAuthException ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
        
        /// <summary>
        /// Initialize firebase connection
        /// </summary>
        private void InitializeFirebase()
        {
            try
            {
                string path = Path.Combine(System.Environment
                    .GetFolderPath(System.Environment.SpecialFolder.ApplicationData), filename);
                GoogleCredential credential;

                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream);

                };

                ChannelCredentials channelCredentials = credential.ToChannelCredentials();

                db = new FirestoreDbBuilder
                {
                    ProjectId = app.Options.ProjectId,
                    ChannelCredentials = channelCredentials

                }.Build();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}