using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTest.Models
{
    public class UserRestaurant
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter a suitable username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email cannot be empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Specify your type")]
        public string Restaurant_Type { get; set; }

        [Required(ErrorMessage = "Please provide the location")]
        public string Location { get; set; }

        public string Is_Approved { get; set; }

        public string Picture { get; set; }



    }
}