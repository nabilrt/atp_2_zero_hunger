using ProjectTest.Models;
using ProjectTest.Operations;
using ProjectTest.Validations;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ProjectTest.Controllers
{
    [EmployeeAuth]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["emp_id"]);
            var employee = UserOperations.getEmployeeDetails(id);
            ViewBag.Name = employee.Name;
            ViewBag.Email = employee.Email;
            ViewBag.Picture = employee.Picture;

            return View(employee);
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
                employee.Picture = "/assets/images/employee.png";

                EmployeeOperations.CreateEmployee(employee);

                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        public ActionResult ViewProfile()
        {
            int id = Convert.ToInt32(Session["emp_id"]);
            var employee = UserOperations.getEmployeeDetails(id);
            ViewBag.Name = employee.Name;
            ViewBag.Email = employee.Email;
            ViewBag.Picture = employee.Picture;

            return View(employee);

        }

        public ActionResult EditProfile()
        {
            int id = Convert.ToInt32(Session["emp_id"]);
            var employee = UserOperations.getEmployeeDetails(id);
            ViewBag.Name = employee.Name;
            ViewBag.Email = employee.Email;
            ViewBag.Picture = employee.Picture;

            return View(employee);
        }

        [HttpPost]
        public ActionResult EditProfile(UserEmployee ue)
        {
            if (ModelState.IsValid)
            {
                if (EmployeeOperations.updateEmployee(ue))
                {
                    return RedirectToAction("ViewProfile");
                }
            }

            return View(ue);
        }

        public ActionResult AssignedRequests()
        {
            int id = Convert.ToInt32(Session["emp_id"]);
            var employee = UserOperations.getEmployeeDetails(id);
            ViewBag.Name = employee.Name;
            ViewBag.Email = employee.Email;
            ViewBag.Picture = employee.Picture;
            var reqs = CollectionOperations.GetCollectionsEmployee(id);
            return View(reqs);
        }

        public ActionResult changeDP()
        {
            int id = Convert.ToInt32(Session["emp_id"]);
            var employee = UserOperations.getEmployeeDetails(id);
            ViewBag.Name = employee.Name;
            ViewBag.Email = employee.Email;
            ViewBag.Picture = employee.Picture;
            return View(employee);
        }

        [HttpPost]
        public ActionResult changeDP(UserEmployee ue, HttpPostedFileBase Picture)
        {
            ViewBag.Error = 0;
            //  if (file != null)
            if (Picture == null)
            {
                ViewBag.Error = 1;
                ViewBag.ErrorMessage = "Please Select an Image";
                return View(ue);
                //ModelState.AddModelError("file", "Please select file to upload.");
            }
            else
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                Picture.SaveAs(path + Path.GetFileName(Picture.FileName));
                var userpath = "/Uploads/" + Picture.FileName;
                EmployeeModel emp = new EmployeeModel();
                emp.Id = ue.Id;
                emp.Picture = userpath;
                if (EmployeeOperations.updateDP(emp))
                {
                    return RedirectToAction("ViewProfile");
                }

            }

            return View(ue);
        }



        public ActionResult Logout()
        {
            Session.Remove("emp_id");

            return RedirectToAction("Login", "Home");
        }
    }
}