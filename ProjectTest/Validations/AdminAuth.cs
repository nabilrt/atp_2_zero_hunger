using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTest.Validations
{
    public class AdminAuth: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["user_id"]!= null)
            {
                return true;
            }

            return false;
        }
    }
}