using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plymouth_Pub.Controllers
{
    public class ManageOrderController : Controller
    {
        // GET: ManageOrder
        public ActionResult Index()
        {
            using (Models.PlymouthEntities db = new Models.PlymouthEntities())
            {
                var result = (from s in db.Orders
                              select s).ToList();

                return View(result);
            }
        }
        public ActionResult Details(int id)
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
        public ActionResult Delete(int id)
        {
            using (Models.PlymouthEntities db = new Models.PlymouthEntities())
            {
                var result = (from s in db.OrderDetails where s.Id == id select s).FirstOrDefault();
                if (result != default(Models.OrderDetail))
                {
                    db.OrderDetails.Remove(result);

                    db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("Order {0} has been deleted succesfully.", result.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["resultMessage"] = "Record not found, cannot be deleted, please check.";
                    return RedirectToAction("Index");
                }
            }
        }
    }
}