using ProjectTest.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectTest.DB;
using ProjectTest.Validations;
using ProjectTest.Models;
using System.IO;
using System.Web.UI.WebControls;

namespace ProjectTest.Controllers
{
    [AdminAuth]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
           
                int id=Convert.ToInt32(Session["user_id"]);
                var admin=UserOperations.getAdminDetails(id);
            ViewBag.Name = admin.Name;
            ViewBag.Email = admin.Email;
            ViewBag.Picture = admin.Picture;

            return View(admin);
            
        }

    //    [AdminAuth]
        public ActionResult JoinRequests()
        {
            int id = Convert.ToInt32(Session["user_id"]);
            var admin = UserOperations.getAdminDetails(id);
            ViewBag.Name=admin.Name;
            ViewBag.Email=admin.Email;
            ViewBag.Picture=admin.Picture;
            var restaurants = RestaurantOperations.GetUnApprovedRestaurants();
            var employees = EmployeeOperations.GetUnApprovedEmployees();
            ViewBag.unapproved = employees;
            return View(restaurants);
            
           // return RedirectToAction("Login", "Home");
        }

        [HttpGet]
       // [Authorize]
        public ActionResult approveRestaurant(int id)
        {

            var restaurent = UserOperations.GetUser(id);
            var hotel = new User();
            hotel.Id=restaurent.Id;
            hotel.Username= restaurent.Username;
            hotel.Email = restaurent.Email;
            hotel.Password = restaurent.Password;
            hotel.User_Type = restaurent.User_Type;
            hotel.Is_Approved = "Yes"; 
              var rest = UserOperations.approveRestaurant(hotel);
                if (rest != null)
                {
                    MailAddress to = new MailAddress(rest.Email);
                    MailAddress from = new MailAddress("19-41607-3@student.aiub.edu");
                    MailMessage message = new MailMessage(from, to);
                    message.Subject = "Approval of Joining Zero Hunger";
                    message.Body = "Congratulations your join request has been approved. Thanks for joining us and contributing the society";
                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587)
                    {
                        Credentials = new NetworkCredential("19-41607-3@student.aiub.edu", "19417243/Nabil"),
                        EnableSsl = true
                        
                    };

                        client.Send(message); 
                        return RedirectToAction("JoinRequests");

             }
         //   return Content("Error");
            // in the beginning of the file





            return Content("");
        }

        public ActionResult rejectRestaurant(int id)
        {
            var restaurent = RestaurantOperations.GetRestaurant(id);
            var userHotel = UserOperations.GetUser(restaurent.User_Id);

            var rest = UserOperations.rejectRestaurant(restaurent);
            if (rest)
            {
                MailAddress to = new MailAddress(userHotel.Email);
                MailAddress from = new MailAddress("19-41607-3@student.aiub.edu");
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Rejection of Joining Zero Hunger";
                message.Body = "Unfortunately your join request cannot be approved. Please ensure you are providing correct informations.";
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    Credentials = new NetworkCredential("19-41607-3@student.aiub.edu", "19417243/Nabil"),
                    EnableSsl = true

                };

                client.Send(message);
                return RedirectToAction("JoinRequests");

            }

            return Content("");

        }

        public ActionResult approveEmployee(int id)
        {

            var employee = UserOperations.GetUser(id);
            var emp = new User();
            emp.Id = employee.Id;
            emp.Username = employee.Username;
            emp.Email = employee.Email;
            emp.Password = employee.Password;
            emp.User_Type = employee.User_Type;
            emp.Is_Approved = "Yes";
            var rest = UserOperations.approveEmployee(emp);
            if (rest != null)
            {
                MailAddress to = new MailAddress(rest.Email);
                MailAddress from = new MailAddress("19-41607-3@student.aiub.edu");
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Approval of Joining Zero Hunger";
                message.Body = "Congratulations your join request has been approved. Thanks for joining us and contributing the society";
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    Credentials = new NetworkCredential("19-41607-3@student.aiub.edu", "19417243/Nabil"),
                    EnableSsl = true

                };

                client.Send(message);
                return RedirectToAction("JoinRequests");

            }
            //   return Content("Error");
            // in the beginning of the file





            return Content("");
        }

        public ActionResult rejectEmployee(int id)
        {
            var employee = EmployeeOperations.GetEmployee(id);
            var userEmployee = UserOperations.GetUser(employee.User_Id);

            var rest = UserOperations.rejectEmployee(employee);
            if (rest)
            {
                MailAddress to = new MailAddress(userEmployee.Email);
                MailAddress from = new MailAddress("19-41607-3@student.aiub.edu");
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Rejection of Joining Zero Hunger";
                message.Body = "Unfortunately your join request cannot be approved. Please ensure you are providing correct informations.";
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    Credentials = new NetworkCredential("19-41607-3@student.aiub.edu", "19417243/Nabil"),
                    EnableSsl = true

                };

                client.Send(message);
                return RedirectToAction("JoinRequests");

            }

            return Content("");

        }

        public ActionResult EditProfile()
        {
            int id = Convert.ToInt32(Session["user_id"]);
            var admin = UserOperations.getAdminDetails(id);
            ViewBag.Name = admin.Name;
            ViewBag.Email = admin.Email;
            ViewBag.Picture = admin.Picture;
            return View(admin);
        }

        [HttpPost]
        public ActionResult EditProfile(UserAdmin ua)
        {
            if (ModelState.IsValid) {
                var useradmin = new UserAdmin();
                useradmin.Id = ua.Id;
                useradmin.Username = ua.Username;
                useradmin.User_Id = ua.User_Id;
                useradmin.Email = ua.Email;
                useradmin.Password = ua.Password;
                useradmin.Name = ua.Name;

                var person = UserOperations.GetUser(ua.User_Id);
                var adm = UserOperations.getAdminDetails(ua.User_Id);

                var user = new User();
                user.Id = ua.User_Id;
                user.Username = ua.Username;
                user.Email = ua.Email;
                user.Password = ua.Password;
                user.Is_Approved = person.Is_Approved;
                user.User_Type = person.User_Type;

                var admin = new Admin();
                admin.Id = ua.Id;
                admin.Name = ua.Name;
                admin.User_Id = ua.User_Id;
                admin.DOB = adm.DOB;
                admin.Picture = adm.Picture;

                if (AdminOperations.updateAdminInfo(user, admin))
                {
                    return RedirectToAction("ViewProfile");

                }
            }


            return View(ua);
        
    }

        public ActionResult ViewProfile()
        {
            int id = Convert.ToInt32(Session["user_id"]);
            var admin = UserOperations.getAdminDetails(id);
            ViewBag.Name = admin.Name;
            ViewBag.Email = admin.Email;
            ViewBag.Picture = admin.Picture;
            return View(admin);
        }

        public ActionResult CollectionRequests()
        {
            int id = Convert.ToInt32(Session["user_id"]);
            var admin = UserOperations.getAdminDetails(id);
            ViewBag.Name = admin.Name;
            ViewBag.Email = admin.Email;
            ViewBag.Picture = admin.Picture;
            var collections = CollectionOperations.GetAllCollections();
            return View(collections);
        }

        public ActionResult AssignEmployee(int id)
        {
            int i = Convert.ToInt32(Session["user_id"]);
            var admin = UserOperations.getAdminDetails(i);
            ViewBag.Name = admin.Name;
            ViewBag.Email = admin.Email;
            ViewBag.Picture = admin.Picture;
            var cols = CollectionOperations.GetCollection(id);
            ViewBag.P_time = cols.Preserved_Time;
            var colls = CollectionDetailOperations.GetCollectionDetails(id);
            ViewBag.colDetails=colls;
            var emps = EmployeeOperations.GetApprovedEmployees();
            ViewBag.employees=emps;
            return View(cols);
        }

        [HttpPost]
        public ActionResult AssignEmployee(CollectionModel cm)
        {
            var empId = cm.Employee_Id;
            if (ModelState.IsValid)
            {
                ViewBag.Error = 0;
                if (empId.ToString() == "")
                {
                    ViewBag.Error = 1;
                    ViewBag.MSG = "Assign an Employee First";

                    return View(cm);


                }

                if (AdminOperations.assignEmployee(cm))
                {
                    var emp = EmployeeOperations.GetEmployee((int)empId);
                    var user = UserOperations.GetUser(emp.User_Id);
                    MailAddress to = new MailAddress(user.Email);
                    MailAddress from = new MailAddress("19-41607-3@student.aiub.edu");
                    MailMessage message = new MailMessage(from, to);
                    message.Subject = "New Delivery Order Assigned";
                    message.Body = "A new collection request delivery has been assigned to you. Please login to your account to check.";
                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587)
                    {
                        Credentials = new NetworkCredential("19-41607-3@student.aiub.edu", "19417243/Nabil"),
                        EnableSsl = true

                    };

                    client.Send(message);

                    return RedirectToAction("CollectionRequests");
                }
            }

            return View(cm);
        }

        public ActionResult changeDP()
        {
            int id = Convert.ToInt32(Session["user_id"]);
            var admin = UserOperations.getAdminDetails(id);
            ViewBag.Name = admin.Name;
            ViewBag.Email = admin.Email;
            ViewBag.Picture = admin.Picture;
            return View(admin);
        }

        [HttpPost]
        public ActionResult changeDP(UserAdmin ua,HttpPostedFileBase Picture)
        {
            ViewBag.Error = 0;
            //  if (file != null)
            if (Picture == null)
            {
                ViewBag.Error = 1;
                ViewBag.ErrorMessage = "Please Select an Image";
                return View(ua);
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
                var userpath="/Uploads/"+Picture.FileName;
                AdminModel ad = new AdminModel();
                ad.Id = ua.Id;
                ad.Picture = userpath;
                if (AdminOperations.updateDP(ad))
                {
                    return RedirectToAction("ViewProfile");
                }

            }
         
            return View(ua);
        }

        public ActionResult AllEmployees()
        {
            int id = Convert.ToInt32(Session["user_id"]);
            var admin = UserOperations.getAdminDetails(id);
            ViewBag.Name = admin.Name;
            ViewBag.Email = admin.Email;
            ViewBag.Picture = admin.Picture;
            var empList = UserOperations.getEmployeeDetails();
           // var workUpdate=CollectionOperations.getWorkUpdates()
            return View(empList);
        }

        public ActionResult workUpdates(int id)
        {
            int i = Convert.ToInt32(Session["user_id"]);
            var admin = UserOperations.getAdminDetails(i);
            ViewBag.Name = admin.Name;
            ViewBag.Email = admin.Email;
            ViewBag.Picture = admin.Picture;
            var workUpdate = CollectionOperations.getWorkUpdates(id);
            return View(workUpdate);

        }

        public ActionResult AllRestaurants()
        {
            int id = Convert.ToInt32(Session["user_id"]);
            var admin = UserOperations.getAdminDetails(id);
            ViewBag.Name = admin.Name;
            ViewBag.Email = admin.Email;
            ViewBag.Picture = admin.Picture;
            var resList = UserOperations.getRestaurantDetails();
            // var workUpdate=CollectionOperations.getWorkUpdates()
            return View(resList);
        }

        public ActionResult currentRequests(int id)
        {
            int i = Convert.ToInt32(Session["user_id"]);
            var admin = UserOperations.getAdminDetails(i);
            ViewBag.Name = admin.Name;
            ViewBag.Email = admin.Email;
            ViewBag.Picture = admin.Picture;
            var workUpdate = CollectionOperations.currentRequests(id);
            return View(workUpdate);

        }

        public ActionResult Logout()
        {
            Session.Remove("user_id");

            return RedirectToAction("Login", "Home");
        }
    }
}