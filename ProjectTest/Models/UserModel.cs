using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTest.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string User_Type { get; set; }
        public string Email { get; set; }
        public string Is_Approved { get; set; }
    }
}