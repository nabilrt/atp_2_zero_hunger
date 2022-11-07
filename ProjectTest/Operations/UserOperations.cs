using ProjectTest.DB;
using ProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace ProjectTest.Operations
{
    public class UserOperations
    {
        public static List <UserModel> GetUsers()
        {
            var users = new List<UserModel>();
            var db=new ZeroHungerDBEntities();

            foreach(var user in db.Users)
            {
                users.Add(new UserModel()
                {
                    Id= user.Id,
                    Username= user.Username,
                    Password= user.Password,
                    User_Type= user.User_Type,
                    Is_Approved= user.Is_Approved,
                });
            }

            return users;
        }

        public static UserModel GetUser(int id)
        {
            var db = new ZeroHungerDBEntities();
            var user = new UserModel();
            var us = db.Users.Find(id);

            user.Id = id;
            user.Username = us.Username;
            user.Email = us.Email;
            user.Password = us.Password;
            user.User_Type = us.User_Type;
            user.Is_Approved = us.Is_Approved;

            return user;
        }

        public static User Create(UserModel user)
        {
            var db = new ZeroHungerDBEntities();
            var newuser = db.Users.Add(new User()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                User_Type = user.User_Type,
                Is_Approved=user.Is_Approved,
            });
            db.SaveChanges();

            return newuser;
        }

        public static UserModel CheckLogin(string username,string password)
        {
            var db = new ZeroHungerDBEntities();
            var user=(from u in db.Users where u.Username == username && u.Password == password select u).SingleOrDefault();
            if (user == null)
            {
                return null;
            }

            var userDetails = GetUser(user.Id);

            return userDetails;

        }

        public static UserAdmin getAdminDetails(int id)
        {
            var db = new ZeroHungerDBEntities();
            var uadmin = new UserAdmin();
            var adminDetails=(from u in db.Users join a in db.Admins on u.Id equals a.User_Id where u.Id == id select new
            {
                adminEmail=u.Email, adminPassword=u.Password,adminUsername=u.Username,adminDOB=a.DOB,adminPicture=a.Picture, adminName=a.Name,adminId=a.Id
            }).SingleOrDefault();
            uadmin.Email = adminDetails.adminEmail;
            uadmin.Password = adminDetails.adminPassword;
            uadmin.User_Id = id;
            uadmin.Username = adminDetails.adminUsername;
            uadmin.DOB = adminDetails.adminDOB;
            uadmin.Picture = adminDetails.adminPicture;
            uadmin.Name = adminDetails.adminName;
            uadmin.Id = adminDetails.adminId;

            return uadmin;
        }

        public static UserModel approveRestaurant(User res)
        {
            var db = new ZeroHungerDBEntities();
            var restaurant = (from u in db.Users where u.Id == res.Id select u).SingleOrDefault();
         //   var restu = new UserModel();
            //   restu = db.Users.Find(id);
            
            db.Entry(restaurant).CurrentValues.SetValues(res);
            
            db.SaveChanges();
            var rest=GetUser(res.Id);
            if (rest != null)
                return rest;
            else
                return null;
            

            

        }
    }
}