using ProjectTest.Models;
using ProjectTest.Operations;
using ProjectTest.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTest.Controllers
{
    [RestaurantAuth]
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Index()
        {

            int id = Convert.ToInt32(Session["rest_id"]);
            var restaurant= UserOperations.getRestaurantDetails(id);
            ViewBag.Name = restaurant.Name;
            ViewBag.Email = restaurant.Email;
            ViewBag.Picture = restaurant.Picture;
            return View(restaurant);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRestaurant ur) {

            if (ModelState.IsValid)
            {
                var user=new UserModel();
                user.Email=ur.Email;
                user.Username=ur.Username;
                user.Password=ur.Password;
                user.User_Type = "Restaurant";
                user.Is_Approved = "No";

                var newUser = UserOperations.Create(user);

                var restaurant = new RestaurantModel();
                restaurant.Name=ur.Name;
                restaurant.Restaurant_Type=ur.Restaurant_Type;
                restaurant.Location=ur.Location;
                restaurant.Picture = "~/assets/default/restaurant.png";
                restaurant.User_Id=newUser.Id;

                RestaurantOperations.Create(restaurant);
                return RedirectToAction("Login","Home");
            }

            return View();
            
           // return View(ur); 
        
        }

        public ActionResult Logout()
        {
            Session.Remove("rest_id");

            return RedirectToAction("Login", "Home");
        }
    }
}