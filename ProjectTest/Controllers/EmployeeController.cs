using ProjectTest.Models;
using ProjectTest.Operations;
using ProjectTest.Validations;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

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
            ViewBag.PendingDeliveriesCount = CollectionOperations.PendingDeliveriesCount(id);
            return View(employee);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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
            var reqs = CollectionOperations.GetCollectionsEmployee(employee.Id);
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
            ViewBag.ErrorMessage = "";
            

            //  if (file != null)
            if (Picture == null)
            {
                ViewBag.Error = 1;
                TempData["ErrorMessage"] = "No Image Selected";
                return RedirectToAction("changeDP");
                //ModelState.AddModelError("file", "Please select file to upload.");
            }
            else if (Picture!=null)
            {
                var supportedTypes = new[] { "jpg", "jpeg", "png" };
                var fileExt = System.IO.Path.GetExtension(Picture.FileName).Substring(1);

                if (!supportedTypes.Contains(fileExt))
                {
                    ViewBag.Error = 1;
                    TempData["ErrorMessage"] = "Please Select an Image";
                    return RedirectToAction("changeDP");
                }


            }
            else if(Picture!=null)
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