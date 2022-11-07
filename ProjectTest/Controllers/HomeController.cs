using ProjectTest.Models;
using ProjectTest.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Error = 0;
                var details=UserOperations.CheckLogin(user.Username,user.Password);

                if (details == null)
                {
                    ViewBag.ErrorMessage = "No Such User Found";
                    ViewBag.Error = 1;
                    return View();
                }
                else
                {
                    if (details.User_Type == "Admin")
                    {
                        Session["user_id"] = details.Id;
                        return RedirectToAction("Index", "Admin");
                    }
                    else if(details.User_Type == "Restaurant" && details.Is_Approved == "Yes")
                    {
                        Session["rest_id"] = details.Id;
                        return RedirectToAction("Index", "Restaurant");

                    }else if(details.User_Type == "Restaurant" && details.Is_Approved == "No")
                    {
                        return RedirectToAction("NotApproved");
                    }
                    if (details.User_Type == "Employee" && details.Is_Approved == "Yes")
                    {
                        Session["emp_id"] = details.Id;
                        return RedirectToAction("Index", "Employee");

                    }
                    else if (details.User_Type == "Employee" && details.Is_Approved == "No")
                    {
                        return RedirectToAction("NotApproved");
                    }
                }
            }

            return View();
        }

        public ActionResult NotApproved()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult WhoToSignUp()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}