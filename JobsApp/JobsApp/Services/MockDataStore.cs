using JobsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsApp.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item 
                { 
                    Id = Guid.NewGuid().ToString(), 
                    Title = "First item",
                    Company = "First company",
                    Location = "Mochudi",
                    Description="This is an item description.",
                    Closing = new DateTime(2020,6,28),
                    Posted = new DateTime(2020,6,23)
                },
                new Item 
                { 
                    Id = Guid.NewGuid().ToString(), 
                    Title = "Second item",
                    Company = "Second company",
                    Description="This is an item description.",
                    Location = "Tlokweng",
                    Closing = new DateTime(2020,1,10),
                    Posted = new DateTime(2020,1,23),
                },
                new Item 
                { 
                    Id = Guid.NewGuid().ToString(), 
                    Title = "Third item",
                    Company = "Third company",
                    Description="This is an item description.",
                    Location = "Kanye",
                    Closing = new DateTime(2020,1,2),
                    Posted = new DateTime(2020,1,10),
                },
                new Item 
                { 
                    Id = Guid.NewGuid().ToString(), 
                    Title = "Fourth item",
                    Company = "Fourth company",
                    Description="This is an item description.",
                    Location = "Francistown",
                    Closing = new DateTime(2020,3,12),
                    Posted = new DateTime(2020,3,15),
                },
                new Item 
                { 
                    Id = Guid.NewGuid().ToString(), 
                    Title = "Fifth item",
                    Company = "Fifth company",
                    Description="This is an item description.",
                    Location = "Maun",
                    Closing = new DateTime(2020,4,5),
                    Posted = new DateTime(2020,4,13)
                },
                new Item 
                { 
                    Id = Guid.NewGuid().ToString(), 
                    Title = "Sixth item",
                    Company = "Sixth company",
                    Description="This is an item description.",
                    Location = "Gaborone",
                    Closing = new DateTime(2020,5,8),
                    Posted = new DateTime(2020,5,12),
                }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}