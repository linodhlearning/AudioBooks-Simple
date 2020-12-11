using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioBooks.Web.Models
{
    public class UserInfoViewModel
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }


        public string Gender { get; set; }
        //public string - birthdate{ get; set; }

        public string Address { get; set; }

    }
}
