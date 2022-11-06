using ProjectTest.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTest.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["user_id"].ToString() != "")
            {
                int id=Convert.ToInt32(Session["user_id"]);
                var admin=UserOperations.getAdminDetails(id);

                return View(admin);

            }

            return RedirectToAction("Login", "Home");
            
        }

        public ActionResult JoinRequests()
        {
            var restaurants = RestaurantOperations.GetUnApprovedRestaurants();
            var employees = EmployeeOperations.GetUnApprovedEmployees();
            ViewBag.unapproved = employees;
            return View(restaurants);
        }

        public ActionResult Logout()
        {
            Session.Remove("user_id");

            return RedirectToAction("Login", "Home");
        }
    }
}