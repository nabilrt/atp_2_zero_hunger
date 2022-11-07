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
        public ActionResult Logout()
        {
            Session.Remove("user_id");

            return RedirectToAction("Login", "Home");
        }
    }
}