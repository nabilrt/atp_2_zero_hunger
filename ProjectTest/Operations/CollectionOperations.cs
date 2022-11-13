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

        public static List<CollectionModel> GetAllCollections()
        {
            var db = new ZeroHungerDBEntities();
            List<CollectionModel> cm = new List<CollectionModel>();
            var collections = (from col in db.Collections where col.Preserved_Time>=DateTime.Now select col).ToList();

            foreach(var col in collections)
            {
                cm.Add(new CollectionModel()
                {
                    Id = col.Id,
                    Restaurant_Id = col.Restaurant_Id,
                    Employee_Id = col.Employee_Id,
                    Preserved_Time = col.Preserved_Time,
                    Status = col.Status,

                });
            }

            return cm;
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


        public static List<CollectionModel> GetCollectionsEmployee(int id)
        {
            var db = new ZeroHungerDBEntities();
            List<CollectionModel> cm = new List<CollectionModel>();
            var colls = (from c in db.Collections where c.Employee_Id == id && c.Preserved_Time>=DateTime.Now select c).ToList();

            foreach (var col in colls)
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

        public static bool updateStatus(CollectionModel cm)
        {
            var db = new ZeroHungerDBEntities();
            var col=(from cl in db.Collections where cl.Id == cm.Id select cl).SingleOrDefault();
            col.Status=cm.Status;
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public static List<CollectionModel> getWorkUpdates(int id)
        {
            var db = new ZeroHungerDBEntities();
            var cols=db.Collections.Where(c => c.Employee_Id == id).ToList();
            var colls=new List<CollectionModel>();  

            foreach (var col in cols)
            {
                colls.Add(new CollectionModel()
                {
                    Id = col.Id,
                    Restaurant_Id = col.Restaurant_Id,
                    Employee_Id = col.Employee_Id,
                    Status = col.Status,
                    Preserved_Time = col.Preserved_Time
                });
            }

            return colls;
        }

        public static List<CollectionModel> currentRequests(int id)
        {
            var db = new ZeroHungerDBEntities();
            var cols = db.Collections.Where(c => c.Restaurant_Id == id).ToList();
            var colls = new List<CollectionModel>();

            foreach (var col in cols)
            {
                colls.Add(new CollectionModel()
                {
                    Id = col.Id,
                    Restaurant_Id = col.Restaurant_Id,
                    Employee_Id = col.Employee_Id,
                    Status = col.Status,
                    Preserved_Time = col.Preserved_Time
                });
            }

            return colls;
        }

        public static int PendingReqCount(int id)
        {
            var db = new ZeroHungerDBEntities();
            return (from r in db.Collections where r.Restaurant_Id == id && r.Status=="Pending" select r).Count();
        }

        public static int DeliveredReqCount(int id)
        {
            var db = new ZeroHungerDBEntities();
            return (from r in db.Collections where r.Restaurant_Id == id && r.Status == "Delivered" select r).Count();
        }

        public static int AcceptedReqCount(int id)
        {
            var db = new ZeroHungerDBEntities();
            return (from r in db.Collections where r.Restaurant_Id == id && r.Status == "Accepted" select r).Count();
        }

        public static int PendingDeliveriesCount(int id)
        {
            var db=new ZeroHungerDBEntities();
            return (from r in db.Collections where r.Employee_Id == id && r.Status != "Delivered" select r).Count();
        }
    }
}