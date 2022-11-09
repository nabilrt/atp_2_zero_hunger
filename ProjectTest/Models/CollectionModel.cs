using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectTest.Models
{
    public class CollectionModel
    {
        public int Id { get; set; }
        public int Restaurant_Id { get; set; }
        public Nullable<int> Employee_Id { get; set; }
        public string Status { get; set; }

        [Required(ErrorMessage ="Preserved Time is mandatory")]
        public System.DateTime Preserved_Time { get; set; }
    }
}