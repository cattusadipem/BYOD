using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BYOD.Models;
using BYOD.ViewModels;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace BYOD.Controllers
{
    [CustomAuthorize]
    public class UsersController : Controller
    {
        //
        // GET: /Users/

        private BYODEntities db = new BYODEntities();

        public ActionResult Index()
        {
            var users = db.USERS.Include(i => i.RULES);
            return View(users);
        }

        public ActionResult Details(int id = 0)
        {

            USERS user = db.USERS.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult AddUser()
        {
            ViewBag.rules_id = new SelectList(db.RULES, "rules_id", "name",
               db.RULES.Single(i => i.name == "user").rules_id);
            return View();
        }

        private static string toMD5(string pass)
        {
            MD5CryptoServiceProvider myMD5 = new MD5CryptoServiceProvider();
            byte[] myPass = System.Text.Encoding.UTF8.GetBytes(pass);
            myPass = myMD5.ComputeHash(myPass);
            StringBuilder s = new StringBuilder();
            foreach (byte p in myPass)
            {
                s.Append(p.ToString("x2").ToLower());
            }
            return s.ToString();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser([Bind(Include = "username, password, name, email, rules_id")]USERS user)
        {
            try
            {
                if (ModelState.IsValid)
                {   
                    db.USERS.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.rules_id = new SelectList(db.RULES, "rules_id", "name", user.rules_id);
            return View(user);
        }
        //[HttpPost]
        //public JsonResult doesUserNameExist(string UserName)
        //{

        //    var user = Membership.GetUser(UserName);

        //    return Json(user == null);
        //}

        [HttpPost]
        public JsonResult check_user(string username)
        {
            var query = from table in db.USERS
                        where table.username == username
                        select table;
            int count = query.Count();
            if (count > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult check_email(string email)
        {
            var query = from table in db.USERS
                        where table.email == email
                        select table;
            int count = query.Count();
            if (count > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditUser(int id = 0)
        {
            USERS user = db.USERS.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.rules_id = new SelectList(db.RULES, "rules_id", "name", user.rules_id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(
           [Bind(Include = "user_id, username, password, name, email, rules_id")]
         USERS user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.rules_id = new SelectList(db.RULES, "rules_id", "name", user.rules_id);
            return View(user);
        }
        //
        // GET: /Instructor/Delete/5

        //public ActionResult DeleteUser(bool? saveChangesError = false, int id = 0)
        //{
        //    if (saveChangesError.GetValueOrDefault())
        //    {
        //        ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
        //    }
        //    USERS user = db.USERS.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //
        // POST: /Instructor/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                USERS user = db.USERS.Find(id);
                db.Entry(user).State = EntityState.Deleted;
                //db.USERS.Remove(user);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //// uncomment dex and log error. 
                //return RedirectToAction("Delete", new { user_id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        //public ActionResult DeleteSelectedUser(IEnumerable<int> userIdToDelete, bool? saveChangesError = false)
        //{
        //    if (saveChangesError.GetValueOrDefault())
        //    {
        //        ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
        //    }

        //    USERS user = new USERS();
        //    foreach (int id in userIdToDelete)
        //    {
        //        user = db.USERS.Find(id);
        //    }

        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        [HttpPost]
        public ActionResult DeleteSelectedUser(IEnumerable<int> userIdToDelete)
        {
            try
            {
                USERS user = new USERS();
                foreach (int id in userIdToDelete)
                {
                    user = db.USERS.Find(id);
                    db.USERS.Remove(user);
                    db.SaveChanges();
                }
            }
            catch (DataException/* dex */)
            {
                // uncomment dex and log error. 
                //return RedirectToAction("Delete", new { user_id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
