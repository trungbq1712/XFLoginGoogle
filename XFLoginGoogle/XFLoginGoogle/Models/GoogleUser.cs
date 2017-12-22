using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFLoginGoogle.Models
{
    public class GoogleUser
    {
        public string NameDisplay { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }
    }
}
