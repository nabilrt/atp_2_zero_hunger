using ProjectTest.DB;
using ProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTest.Operations
{
    public class AdminOperations
    {
        public static bool updateAdminInfo(User us, Admin ad)
        {
            var db=new ZeroHungerDBEntities();
            var usr=(from u in db.Users where u.Id == us.Id select u).FirstOrDefault();
            var adm=(from a in db.Admins where a.Id == ad.Id select a).FirstOrDefault();

            db.Entry(usr).CurrentValues.SetValues(us);
            db.Entry(adm).CurrentValues.SetValues(ad);

            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;

        }

        public static bool assignEmployee(CollectionModel cm)
        {
            var db = new ZeroHungerDBEntities();
            var collection=(from col in db.Collections where col.Id==cm.Id select col).SingleOrDefault();
            collection.Employee_Id = cm.Employee_Id;
            collection.Status = "Accepted";
            if (db.SaveChanges() > 0)
            {
                return true;

            }

            return false;
            
        }
    }
}