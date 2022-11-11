using ProjectTest.DB;
using ProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTest.Operations
{
    public class RestaurantOperations
    {
        public static List<RestaurantModel> GetRestaurants()
        {
            var hotels=new List<RestaurantModel>();
            var db=new ZeroHungerDBEntities();

            foreach (var hotel in db.Restaurants)
            {
                hotels.Add(new RestaurantModel()
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    Restaurant_Type = hotel.Restaurant_Type,
                    Location = hotel.Location,
                    Picture = hotel.Picture,
                    User_Id = hotel.User_Id,
                });
            }

            return hotels;
        }

        public static RestaurantModel GetRestaurant(int id)
        {
            var db = new ZeroHungerDBEntities();
            var restaurant =new RestaurantModel();
            var rest=db.Restaurants.SingleOrDefault(h => h.Id == id);

            restaurant.Id= rest.Id;
            restaurant.Name= rest.Name;
            restaurant.Location= rest.Location;
            restaurant.Picture= rest.Picture;
            restaurant.User_Id= rest.User_Id;
            restaurant.Restaurant_Type = rest.Restaurant_Type;

            return restaurant;


        }

        public static void Create(RestaurantModel restaurant)
        {
            var db = new ZeroHungerDBEntities();
            db.Restaurants.Add(new Restaurant()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Location = restaurant.Location,
                Picture = restaurant.Picture,
                User_Id = restaurant.User_Id,
                Restaurant_Type = restaurant.Restaurant_Type
            });

            db.SaveChanges();
        }

        public static List<UserRestaurant> GetUnApprovedRestaurants()
        {
            var db=new ZeroHungerDBEntities();
            var rests=new List<UserRestaurant>();   
            var unapproved=(from u in db.Users join r in db.Restaurants on u.Id equals r.User_Id where u.Is_Approved == "No" select new
            {
                resUsername=u.Username, resEmail=u.Email, resName=r.Name, resLocation=r.Location, resPicture=r.Picture, app_status=u.Is_Approved,resUserId=r.User_Id,
                resId=r.Id
            }).ToList();

            foreach(var restaurant in unapproved)
            {
                rests.Add(new UserRestaurant()
                {
                    Id=restaurant.resId,
                    Username = restaurant.resUsername,
                    Email = restaurant.resEmail,
                    Name = restaurant.resName,
                    Location = restaurant.resLocation,
                    Picture = restaurant.resPicture,
                    Is_Approved = restaurant.app_status,
                    User_Id=restaurant.resUserId

                });
            }

            return rests;
        }

        public static bool updateRestaurant(UserRestaurant ur)
        {
            var db = new ZeroHungerDBEntities();
            var user = (from u in db.Users where u.Id == ur.User_Id select u).SingleOrDefault();
            var restaurant=(from r in db.Restaurants where r.Id==ur.Id select r).SingleOrDefault();

            user.Email = ur.Email;
            user.Password = ur.Password;
            restaurant.Name = ur.Name;
            user.Username= ur.Username;

            if (db.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public static bool updateDP(RestaurantModel rm)
        {
            var db = new ZeroHungerDBEntities();
            var restaurant = (from r in db.Restaurants where r.Id == rm.Id select r).SingleOrDefault();
            restaurant.Picture=rm.Picture;
            if (db.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }
    }
}