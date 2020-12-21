using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Apis.Auth;
using Google.Cloud.Firestore.V1;
using Grpc.Auth;
using Grpc.Core;
using JobsApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;


namespace XUnitTestProject
{
    public class TestFirebase
    {
        private FirestoreDb db;
       // private FirebaseAuth auth;

        public TestFirebase()
        {
            //Initialize Connection
            GoogleCredential credential;

            using (var stream = new FileStream("jobsapp-c8100-7dd3e3e004b2.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream);
                    
            };

            ChannelCredentials channelCredentials = credential.ToChannelCredentials();

            db = new FirestoreDbBuilder
            {
                ProjectId = "jobsapp-c8100",
                ChannelCredentials = channelCredentials
                  
                 
            }.Build();
        }
        /// <summary>
        /// Test FindAll
        /// </summary>
        [Fact]
        public async void Firebase_FindAllAsync()
        {
            List<Item> result = new List<Item>();

            //jobstest collection contains two items
            CollectionReference collection = db.Collection("jobstest");
            QuerySnapshot documents = await collection.GetSnapshotAsync();

            foreach (DocumentSnapshot document in documents)
            {
                result.Add(document.ConvertTo<Item>());
            }            

            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
            
        }
        /// <summary>
        /// Test find by document ID
        /// </summary>
        [Fact]
        public async void Firebase_FindAsync()
        {
            const string documentId = "NUgjbsX472qNOTsmeBuA";

            CollectionReference collection = db.Collection("jobstest");
            DocumentSnapshot document = await collection.Document(documentId).GetSnapshotAsync();
            Item item = document.ConvertTo<Item>();

            Assert.NotNull(item);
            Assert.Equal("principal resources officer", item.Title);
            Assert.Equal(5, item.Competencies.Count);

        }
 
    }
}
