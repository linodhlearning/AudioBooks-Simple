using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBooks.Web.Models
{
    public class AuthInfoViewModel
    {
        public string IdToken { get; set; }
        public string AccesToken { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Operations { get; set; }

    }
}
