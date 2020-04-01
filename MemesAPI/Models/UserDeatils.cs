using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemesAPI.Models
{
    public class UserDeatils
    {
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public String Role { get; set; }
        public String OldPassword { get; set; }
    }
}