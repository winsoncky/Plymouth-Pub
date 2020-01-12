using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plymouth_Pub.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (Models.PlymouthEntities db = new Models.PlymouthEntities())
            {
                var result = (from s in db.Products select s).ToList();
                return View(result);
            }
        }

        public ActionResult Index2()
        {
            return Content(
                "<html><body><h1>Message</h1></body></html>"
                );
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}