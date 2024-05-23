using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VATWebAPI.Models
{
    public class UserModel
    {
        public string UserId { get; set; }

        public string FullName { get; set; }
  

        public Dictionary<string, string> Claims { get; set; }
    }
}