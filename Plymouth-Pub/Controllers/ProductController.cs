using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plymouth_Pub.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<Models.Product> result = new List<Models.Product>();

            ViewBag.ResultMessage = TempData["ResultMessage"];

            using (Models.PlymouthEntities db = new Models.PlymouthEntities())
            {
                result = (from s in db.Products select s).ToList();
                return View(result);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Models.Product postback, HttpPostedFileBase File)
        {
            string Picture = null;
            if (File != null)
            {
                Picture = System.IO.Path.GetFileName(File.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/images/"), Picture);
                // file is uploaded
                File.SaveAs(path);
            }
            postback.DefaultImageURL = Picture;
            if (this.ModelState.IsValid)
            {
                using (Models.PlymouthEntities db = new Models.PlymouthEntities())
                {
                    db.Products.Add(postback);
                    db.SaveChanges();
                    TempData["ResultMessage"] = String.Format("Product {0} has been successfully created", postback.Name);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ResultMessage = "Information not correct, please check.";
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            using (Models.PlymouthEntities db = new Models.PlymouthEntities())
            {
                var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();
                if (result != default(Models.Product))
                {
                    return View(result);
                }
                else
                {
                    TempData["ResultMessage"] = "Information not correct, please check.";
                    return RedirectToAction("Index");
                }
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Models.Product postback, HttpPostedFileBase file)
        {
            string Picture = null;
            if (file != null)
            {
                Picture = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/images/"), Picture);
                // file is uploaded
                file.SaveAs(path);

            }
            postback.DefaultImageURL = file != null ? Picture : postback.DefaultImageURL;
            if (this.ModelState.IsValid)
            {
                using (Models.PlymouthEntities db = new Models.PlymouthEntities())
                {
                    var result = (from s in db.Products where s.Id == postback.Id select s).FirstOrDefault();

                    result.Name = postback.Name; result.Price = postback.Price;
                    result.PublishDate = postback.PublishDate; result.Quantity = postback.Quantity;
                    result.Status = postback.Status; result.CategoryId = postback.CategoryId;
                    result.DefaultImageId = postback.DefaultImageId; result.Description = postback.Description;
                    result.DefaultImageURL = postback.DefaultImageURL;

                    db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("Product {0} has been successfully edited", postback.Name);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(postback);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (Models.PlymouthEntities db = new Models.PlymouthEntities())
            {
                var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();
                if (result != default(Models.Product))
                {
                    db.Products.Remove(result);

                    db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("Product {0} has been deleted succesfully.", result.Name);
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
