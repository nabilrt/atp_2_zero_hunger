using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTest.Models
{
    public class RestaurantModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Restaurant_Type { get; set; }
        public string Location { get; set; }
        public int User_Id { get; set; }
        public string Picture { get; set; }
    }
}