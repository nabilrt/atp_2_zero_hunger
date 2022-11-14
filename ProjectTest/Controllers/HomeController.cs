using ProjectTest.Models;
using ProjectTest.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using ProjectTest.Validations;

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

        public ActionResult ForgotPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPass(string email)
        {
            ViewBag.Error = 0;
            if(email == "")
            {
                ViewBag.Error = 1;
                ViewBag.MSG = "Please Enter Something";
                return View();
            }
            else
            {
                if (UserOperations.checkEmail(email))
                {
                    Random r = new Random();
                    var otp = r.Next(1000, 9999);
                    Session["OTP"] =otp;
                   Session["email"] = email;
                    MailAddress to = new MailAddress(email);
                    MailAddress from = new MailAddress(Credentials.Credentials.From);
                    MailMessage message = new MailMessage(from, to);
                    message.Subject = "Password Reset ZH";
                    message.Body = "Dear Client, Your Password Reset OTP is "+otp;
                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587)
                    {
                        Credentials = new NetworkCredential(Credentials.Credentials.Email, Credentials.Credentials.Password),
                        EnableSsl = true

                    };

                    client.Send(message);
                  //  ViewBag.Email=email;
                    return RedirectToAction("OTPVerification");
                }
            }

            return View();
        }

        [ForgotPassAuth]
        public ActionResult OTPVerification()
        {
            return View();
        }


        [HttpPost]
        [ForgotPassAuth]
        public ActionResult OTPVerification(string otp,string email)
        {

            ViewBag.Error = 0;
            if (otp == "")
            {
                ViewBag.Error = 1;
                ViewBag.MSG = "Please Enter Something";
                return View();
            }
            else
            {
                if(Convert.ToInt32(otp) == Convert.ToInt32(Session["otp"]))
                {
                    ViewBag.Email = email;
                    return RedirectToAction("ChangePassword");

                }
                ViewBag.Error = 1;
                ViewBag.MSG = "Wrong OTP!";
                return View();
            }
          //  return View();
        }

        [ForgotPassAuth]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ForgotPassAuth]
        public ActionResult ChangePassword(string password, string email)
        {
            ViewBag.Error = 0;
            if (password == "")
            {
                ViewBag.Error = 1;
                ViewBag.MSG = "Please Enter Something";
                return View();
            }
            else
            {
                if (password.Length >= 5)
                {
                    if (UserOperations.changePassword(email, password))
                    {
                        Session.Remove("otp");
                        Session.Remove("email");
                        return RedirectToAction("ChangeConfirmation");
                    }
                }
                ViewBag.Error = 1;
                ViewBag.MSG = "Password Must Be a Length of 5 or more!";
                return View();
            }

        }
        [ForgotPassAuth]
        public ActionResult ChangeConfirmation()
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