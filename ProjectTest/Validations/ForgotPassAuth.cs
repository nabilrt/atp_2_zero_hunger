using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTest.Validations
{
    public class ForgotPassAuth:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["otp"] != null && httpContext.Session["email"]!=null)
            {
                return true;
            }

            return false;
        }
    }
}