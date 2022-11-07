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

        public static UserEmployee getEmployeeDetails(int id)
        {
            var db = new ZeroHungerDBEntities();
            var uemployee = new UserEmployee();
            var employeeDetails = (from u in db.Users
                                join a in db.Employees on u.Id equals a.User_Id
                                where u.Id == id
                                select new
                                {
                                    employeeEmail = u.Email,
                                    employeePassword = u.Password,
                                    employeeUsername = u.Username,
                                    employeeDOB = a.DOB,
                                    employeePicture = a.Picture,
                                    employeeName = a.Name,
                                    employeeId = a.Id,
                                    employeeGender=a.Gender,
                                }).SingleOrDefault();
            uemployee.Email = employeeDetails.employeeEmail;
            uemployee.Password = employeeDetails.employeePassword;
            uemployee.User_Id = id;
            uemployee.Username = employeeDetails.employeeUsername;
            uemployee.DOB = employeeDetails.employeeDOB;
            uemployee.Picture = employeeDetails.employeePicture;
            uemployee.Name = employeeDetails.employeeName;
            uemployee.Id = employeeDetails.employeeId;
            uemployee.Gender=employeeDetails.employeeGender;
            return uemployee;
        }

        public static UserRestaurant getRestaurantDetails(int id)
        {
            var db = new ZeroHungerDBEntities();
            var urestaurant = new UserRestaurant();
            var restaurantDetails = (from u in db.Users
                                   join a in db.Restaurants on u.Id equals a.User_Id
                                   where u.Id == id
                                   select new
                                   {
                                       restEmail = u.Email,
                                       restPassword = u.Password,
                                       restUsername = u.Username,
                                       restLocation = a.Location,
                                       restPicture = a.Picture,
                                       restName = a.Name,
                                       restId = a.Id,
                                       restType = a.Restaurant_Type,
                                   }).SingleOrDefault();
            urestaurant.Email = restaurantDetails.restEmail;
            urestaurant.Password = restaurantDetails.restPassword;
            urestaurant.User_Id = id;
            urestaurant.Username = restaurantDetails.restUsername;
            urestaurant.Location = restaurantDetails.restLocation;
            urestaurant.Picture = restaurantDetails.restPicture;
            urestaurant.Name = restaurantDetails.restName;
            urestaurant.Id = restaurantDetails.restId;
            urestaurant.Restaurant_Type = restaurantDetails.restType;
            return urestaurant;
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

        public static UserModel approveEmployee(User res)
        {
            var db = new ZeroHungerDBEntities();
            var employee = (from u in db.Users where u.Id == res.Id select u).SingleOrDefault();
            //   var restu = new UserModel();
            //   restu = db.Users.Find(id);

            db.Entry(employee).CurrentValues.SetValues(res);

            db.SaveChanges();
            var rest = GetUser(res.Id);
            if (rest != null)
                return rest;
            else
                return null;




        }

        public static bool rejectRestaurant(RestaurantModel res)
        {
            var db = new ZeroHungerDBEntities();
            var rest = (from u in db.Restaurants where u.Id == res.Id select u).SingleOrDefault();
            var user=(from u in db.Users where u.Id==rest.User_Id select u).SingleOrDefault();
            db.Restaurants.Remove(rest);
            db.Users.Remove(user);
            if (db.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public static bool rejectEmployee(EmployeeModel res)
        {
            var db = new ZeroHungerDBEntities();
            var emps = (from u in db.Employees where u.Id == res.Id select u).SingleOrDefault();
            var user = (from u in db.Users where u.Id == emps.User_Id select u).SingleOrDefault();
            db.Employees.Remove(emps);
            db.Users.Remove(user);
            if (db.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }
    }
}