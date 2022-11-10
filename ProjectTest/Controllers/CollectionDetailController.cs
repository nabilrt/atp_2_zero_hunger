using ProjectTest.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTest.Controllers
{
    public class CollectionDetailController : Controller
    {
        // GET: CollectionDetail
        public ActionResult Index(int id)
        {
            int i = Convert.ToInt32(Session["rest_id"]);
            var restaurant = UserOperations.getRestaurantDetails(i);
            ViewBag.Name = restaurant.Name;
            ViewBag.Email = restaurant.Email;
            ViewBag.Picture = restaurant.Picture;
            var cols=CollectionOperations.GetCollection(id);
            ViewBag.P_time = cols.Preserved_Time;
            var colls=CollectionDetailOperations.GetCollectionDetails(id);
            return View(colls);
        }
    }
}