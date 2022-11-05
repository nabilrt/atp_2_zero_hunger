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
    }
}