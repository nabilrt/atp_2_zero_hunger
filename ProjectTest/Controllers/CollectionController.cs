using ProjectTest.Models;
using ProjectTest.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTest.Controllers
{
    public class CollectionController : Controller
    {
        // GET: Collection
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["rest_id"]);
            var restaurant = UserOperations.getRestaurantDetails(id);
            ViewBag.Name = restaurant.Name;
            ViewBag.Email = restaurant.Email;
            ViewBag.Picture = restaurant.Picture;
            return View();
        }

        [HttpPost]
        public ActionResult Index(CollectionModel cm, string[] foods)
        {
             List<string> allFoods=new List<string>();
            
            if (ModelState.IsValid)
            {
                ViewBag.Error = 0;

                if (foods==null)
                {
                    ViewBag.Error = 1;
                    ViewBag.MSG = "Please Choose Food First!";
                    return View();

                }

                foreach (string food in foods)
                {
                    allFoods.Add(food);
                }

                if(CollectionOperations.createRequest(cm, allFoods))
                {
                    return RedirectToAction("RequestHistory");
                }

                return Content("");

            }
            return View(cm);
        }

        public ActionResult RequestHistory()
        {
            int id = Convert.ToInt32(Session["rest_id"]);
            var restaurant = UserOperations.getRestaurantDetails(id);
            ViewBag.Name = restaurant.Name;
            ViewBag.Email = restaurant.Email;
            ViewBag.Picture = restaurant.Picture;
            var requests=CollectionOperations.GetCollections(id);
            return View(requests);
        }

        public ActionResult updateStatus(int id)
        {
            int i = Convert.ToInt32(Session["emp_id"]);
            var employee = UserOperations.getEmployeeDetails(i);
            ViewBag.Name = employee.Name;
            ViewBag.Email = employee.Email;
            ViewBag.Picture = employee.Picture;
            var cols = CollectionOperations.GetCollection(id);
            ViewBag.P_time = cols.Preserved_Time;
            var colls = CollectionDetailOperations.GetCollectionDetails(id);
            ViewBag.colDetails = colls;
            var emps = EmployeeOperations.GetApprovedEmployees();
            ViewBag.employees = emps;

            return View(cols);
        }

        [HttpPost]
        public ActionResult updateStatus(CollectionModel cm)
        {
            if (CollectionOperations.updateStatus(cm))
            {
                return RedirectToAction("AssignedRequests", "Employee");
            }

            return View(cm);
        }


    }
}