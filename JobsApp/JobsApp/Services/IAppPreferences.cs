using JobsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobsApp.Services
{
    public interface IAppPreferences
    {
        void SaveLike(Item item);
        void DeleteLike(Item item);
        List<Item> GetLikes();
        bool IsLiked(Item item);
    }
}
