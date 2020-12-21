using System;
using System.Collections.Generic;
using System.Text;

namespace JobsApp.Services
{
    public interface IAppManager
    {
        void Show(string message);
        bool IsLoggedIn();
        void SignOut();
    }
}
