using ProjectTest.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectTest.DB;

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
            if (Session["user_id"].ToString() != "") { 
            var restaurants = RestaurantOperations.GetUnApprovedRestaurants();
            var employees = EmployeeOperations.GetUnApprovedEmployees();
            ViewBag.unapproved = employees;
            return View(restaurants);
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
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
                        return Content(restaurent.Username);

             }
         //   return Content("Error");
            // in the beginning of the file





            return Content("");
        }

        public ActionResult Logout()
        {
            Session.Remove("user_id");

            return RedirectToAction("Login", "Home");
        }
    }
}