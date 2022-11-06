using ProjectTest.Models;
using ProjectTest.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTest.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserEmployee uemp)
        {
            if (ModelState.IsValid)
            {
                var user = new UserModel();
                user.Email = uemp.Email;
                user.Username = uemp.Username;
                user.Password = uemp.Password;
                user.User_Type = "Employee";
                user.Is_Approved = "No";

                var newUser = UserOperations.Create(user);

                var employee = new EmployeeModel();
                employee.Name=uemp.Name;
                employee.Gender = uemp.Gender;
                employee.DOB=uemp.DOB;
                employee.User_Id=newUser.Id;
                employee.Picture = "~/assets/images/employee.png";

                EmployeeOperations.CreateEmployee(employee);

                return RedirectToAction("Login", "Home");
            }

            return View();
        }
    }
}