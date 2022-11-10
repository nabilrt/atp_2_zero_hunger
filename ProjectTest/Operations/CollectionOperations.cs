using ProjectTest.DB;
using ProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTest.Operations
{
    public class CollectionOperations
    {
        public static bool createRequest(CollectionModel cm, List<string> foods)
        {
            var db=new ZeroHungerDBEntities();
            var newCollection = db.Collections.Add(new Collection()
            {
             //   Id = cm.Id,
                Restaurant_Id = cm.Restaurant_Id,
                Status = "Pending",
                Preserved_Time = cm.Preserved_Time,

            });

            db.SaveChanges();

            foreach (var food in foods)
            {
                db.CollectionDetails.Add(new CollectionDetail()
                {
                    Collection_Id = newCollection.Id,
                    Food_Name = food
                });
            }

            if (db.SaveChanges() > 0)
            {
                return true;
            }

            return false;

        }

        public static List<CollectionModel> GetCollections(int id)
        {
            var db = new ZeroHungerDBEntities();
            List<CollectionModel> cm = new List<CollectionModel>();
            var colls = (from c in db.Collections where c.Restaurant_Id == id select c).ToList();

            foreach(var col in colls)
            {
                cm.Add(new CollectionModel()
                {
                    Id = col.Id,
                    Restaurant_Id = col.Restaurant_Id,
                    Employee_Id = col.Employee_Id,
                    Status = col.Status,
                    Preserved_Time = col.Preserved_Time,
                });
            }

            return cm;

        }

        public static CollectionModel GetCollection(int id)
        {
            var db = new ZeroHungerDBEntities();
            var cm = new CollectionModel();
            var col = (from c in db.Collections where c.Id == id select c).SingleOrDefault();
            cm.Id=col.Id;
            cm.Restaurant_Id=col.Restaurant_Id;
            cm.Employee_Id=col.Employee_Id;
            cm.Status=col.Status;
            cm.Preserved_Time = col.Preserved_Time;

            return cm;

        }

       
    }
}