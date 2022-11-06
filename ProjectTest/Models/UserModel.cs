using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTest.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Username is Required")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }
        public string User_Type { get; set; }
        public string Email { get; set; }
        public string Is_Approved { get; set; }
    }
}