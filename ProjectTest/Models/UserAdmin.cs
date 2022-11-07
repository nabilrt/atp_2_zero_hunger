using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTest.Models
{
    public class UserAdmin
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public System.DateTime DOB { get; set; }
        public int User_Id { get; set; }
        public string Picture { get; set; }

        [Required]
        public string Username { get; set; }
       [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
      //  public string User_Type { get; set; }
        public string Email { get; set; }
    }
}