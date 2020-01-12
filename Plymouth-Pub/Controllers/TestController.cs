using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plymouth_Pub.Controllers
{
    public class TestController : Controller
    {
        public ActionResult GetCart()
        {
            var cart = Models.Cart.Operation.GetCurrentCart();
            cart.AddProduct(1);

            return Content(string.Format("Current amount: $ {0}", cart.TotalAmount));
        }

        public ActionResult TestBootStrap()
        {
            return View();
        }



    }
}