using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTest.Models
{
    public class AdminModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime DOB { get; set; }
        public int User_Id { get; set; }
        public string Picture { get; set; }
    }
}