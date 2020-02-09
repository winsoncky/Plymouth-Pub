using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plymouth_Pub.Controllers
{
    public class ManageUserController : Controller
    {
        [Authorize(Roles = "Admin")]
        // GET: ManageUser
        public ActionResult Index()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];
            using(Models.UserEntities db = new Models.UserEntities())
            {   
                var result = (from s in db.AspNetUsers
                              select new Models.ManageUser
                              {
                                  Id = s.Id,
                                  UserName = s.UserName,
                                  Email = s.Email
                              }).ToList();
                return View(result);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == id
                              select new Models.ManageUser
                              {
                                  Id = s.Id,
                                  UserName = s.UserName,
                                  Email = s.Email
                              }).FirstOrDefault();
                if (result != default(Models.ManageUser))
                {
                    return View(result);
                }
            }
            TempData["ResultMessage"] = String.Format("User {0}] not exist, please try again.", id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Models.ManageUser postback)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == postback.Id
                              select s).FirstOrDefault();
                if (result != default(Models.AspNetUsers))
                {
                    result.UserName = postback.UserName;
                    result.Email = postback.Email;
                    db.SaveChanges();
                    TempData["ResultMessage"] = String.Format("User {0} has been sucessfully edited.", postback.UserName);
                    return RedirectToAction("Index");
                }
            }
            TempData["ResultMessage"] = String.Format("User {0} not exist, please try again.", postback.UserName);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Models.ManageUser postback)
        {
            using (Models.UserEntities db = new Models.UserEntities())
            {
                var result = (from s in db.AspNetUsers
                              where s.Id == postback.Id
                              select s).FirstOrDefault();
                if (result != default(Models.AspNetUsers))
                {
                    db.AspNetUsers.Remove(result);

                    db.SaveChanges();

                    TempData["ResultMessage"] = String.Format("User {0} has been sucessfully edited.", postback.UserName);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ResultMessage"] = String.Format("User {0} not exist, please try again.", postback.UserName);
                    return RedirectToAction("Index");
                }
            }
        }
    }
}