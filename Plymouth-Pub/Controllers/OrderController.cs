using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace Plymouth_Pub.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize()]
        public ActionResult Index(Models.OrderModel.Table postback)
        {
            if( this.ModelState.IsValid)
            {
                var currentcart = Models.Cart.Operation.GetCurrentCart();
                var userId = HttpContext.User.Identity.GetUserId();

                using (Models.PlymouthEntities db = new Models.PlymouthEntities())
                {
                    var order = new Models.Order()
                    {
                        UserId = userId,
                        TableNumber = postback.TableNumber
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();

                    var orderDetails = currentcart.ToOrderDetailList(order.Id);

                    db.OrderDetails.AddRange(orderDetails);
                    db.SaveChanges();
                }
                return Content("Order Sucess!");
            }
            return View();
        }

        public ActionResult MyOrder()
        {
            var userId = HttpContext.User.Identity.GetUserId();

            using (Models.PlymouthEntities db = new Models.PlymouthEntities())
            {
                var result = (from s in db.Orders
                              where s.UserId == userId
                              select s).ToList();

                return View(result);
            }
        }

        public ActionResult MyOrderDetail(int id)
        {
            using (Models.PlymouthEntities db = new Models.PlymouthEntities())
            {
                var result = (from s in db.OrderDetails
                              where s.OrderId == id
                              select s).ToList();

                if (result.Count == 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }


    }
}