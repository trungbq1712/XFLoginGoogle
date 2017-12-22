using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFLoginGoogle.Models;

namespace XFLoginGoogle.Services
{
    public interface IGoogleService
    {
        void LogIn(Action<GoogleUser, string> onLoginComplete);
        void LogOut();
    }
}
