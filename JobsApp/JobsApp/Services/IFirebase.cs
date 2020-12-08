using JobsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobsApp.Services
{
    public interface IFirebase
    {
        Task<Item> FindAsync(string id);
        Task<IEnumerable<Item>> FindAllAsync(bool forceRefresh = false);
    }
}
