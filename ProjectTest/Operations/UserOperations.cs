using ProjectTest.DB;
using ProjectTest.Models;
using System;
using System.Collections.Generic;
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
    }
}