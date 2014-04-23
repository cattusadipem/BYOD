//using BYOD.Models;
//using BYOD.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Net;
//using System.Web.Mvc;

//namespace BYOD.Controllers
//{
//    [CustomAuthorize]
//    public class DevicesController : Controller
//    {
//        private BYODEntities db = new BYODEntities();

//        // GET: /Devices/
//        public ActionResult Index()
//        {
//            List<DEVICES> devices = new List<DEVICES>();
//            devices = db.DEVICES.ToList();
//            return View(devices);
//        }

//        // GET: /Devices/AddDevice
//        public ActionResult AddDevice()
//        {
//            UserViewModel deviceViewModel = new UserViewModel();

//            //Get data for RULES DropdownList
//            IEnumerable<SelectListItem> ruleList = (from p in db.RULES
//                                                    select p).AsEnumerable().Select(m => new SelectListItem()
//                                                    {
//                                                        Text = m.name,
//                                                        Value = m.rules_id.ToString()
//                                                    });

//            deviceViewModel.rules_list = new SelectList(ruleList, "Value", "Text");

//            return View(deviceViewModel);
//        }

//        //POST: /Devices/AddDevice
//        // To protect from overposting attacks
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult AddDevice([Bind(Include = "user_id, rules_id, description")]DEVICES device, string username, UserViewModel model)
//        {
//            USERS user = new USERS();

//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    device.user_id = db.USERS.Where(x => x.username.Equals(username)).Select(y => y.user_id).FirstOrDefault();
//                    //device.rules_id = model.selectedId;

//                    db.DEVICES.Add(device);
//                    db.SaveChanges();
//                    return RedirectToAction("Index");
//                }
//            }
//            catch (Exception)
//            {
//                ModelState.AddModelError("", "Unable to save changes. An error has occurred.");
//            }
//            return View();
//        }

//        public ActionResult Manager()
//        {
//            return View();
//        }

//        //GET: /Devices/Manager/?
//        //public ActionResult Manager(int? id)
//        //{
//        //    if (id == null)
//        //    {
//        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//        //    }

//        //    DEVICES device = db.DEVICES.Find(id);

//        //    if (device == null)
//        //    {
//        //        return HttpNotFound();
//        //    }

//        //    return View(device);
//        //}

//        //GET: /Devices/Edit/?
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            UserViewModel deviceViewModel = new UserViewModel();

//            //Get data for RULES DropdownList
//            IEnumerable<SelectListItem> ruleList = (from p in db.RULES
//                                                    select p).AsEnumerable().Select(m => new SelectListItem()
//                                                    {
//                                                        Text = m.name,
//                                                        Value = m.rules_id.ToString()
//                                                    });

//            deviceViewModel.rules_list = new SelectList(ruleList, "Value", "Text");

//            deviceViewModel.users = db.DEVICES.Find(id);

//            if (deviceViewModel.users == null)
//            {
//                return HttpNotFound();
//            }

//            return View(deviceViewModel);
//        }

//        //POST: /Devices/Edit/?
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "device_id, rules_id, description")]DEVICES device, UserViewModel model)
//        {
//            try
//            {
//                var excluded = new[] { "user_id", "name", "phone_number", "status", "model", "imei", "platform", "last_login_time", "date_added", "date_stolen", "isstolen", "isremove", "date_remove", "iswipe", "date_wipe", "reg_id" };

//                device.device_id = model.users.device_id;
//                //device.rules_id = model.devices.rules_id;
//                device.description = model.users.description;

//                if (ModelState.IsValid)
//                {
//                    db.Entry(device).State = EntityState.Modified;

//                    foreach (var name in excluded)
//                    {
//                        db.Entry(device).Property(name).IsModified = false;
//                    }

//                    db.SaveChanges();
//                    return RedirectToAction("Index");
//                }
//            }
//            catch (Exception)
//            {
//                ModelState.AddModelError("", "Unable to save changes. An error has occurred.");
//            }
//            return View();
//        }

//        //GET: /Devices/Delete/?
//        public ActionResult Delete(int? id, bool? saveChangesError = false)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            if (saveChangesError.GetValueOrDefault())
//            {
//                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
//            }

//            DEVICES device = db.DEVICES.Find(id);

//            if (device == null)
//            {
//                return HttpNotFound();
//            }

//            return View(device);
//        }

//        //POST: /Devices/Delete/?
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Delete(int id)
//        {
//            try
//            {
//                DEVICES device = db.DEVICES.Find(id);
//                db.DEVICES.Remove(device);
//                db.SaveChanges();
//            }
//            catch (Exception)
//            {
//                ModelState.AddModelError("", "Unable to save changes. An error has occurred.");
//                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
//            }
//            return RedirectToAction("Index");
//        }

//        //POST: Multiple delete
//        [HttpPost]
//        public ActionResult DeleteSelected(IEnumerable<int> deviceIdToDelete)
//        {
//            try
//            {
//                DEVICES device = new DEVICES();
//                foreach (int id in deviceIdToDelete)
//                {
//                    device = db.DEVICES.Find(id);
//                    db.DEVICES.Remove(device);
//                    db.SaveChanges();

//                }
//            }
//            catch (DataException/* dex */)
//            {
//                // uncomment dex and log error. 
//                //return RedirectToAction("Delete", new { user_id = id, saveChangesError = true });
//            }
//            return RedirectToAction("Index");
//        }

//        public ActionResult SendMessage()
//        {
//            return View();
//        }

//        public ActionResult LockDevice()
//        {
//            return View();
//        }

//        public ActionResult WipeDevice()
//        {
//            return View();
//        }

//        public ActionResult FindDevice()
//        {
//            return View();
//        }

//        public ActionResult TrackDevice()
//        {
//            return View();
//        }

//        //Ensuring that Database Connections Are Not Left Open
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        //Other methods
//        public JsonResult GetUsers(string term)
//        {
//            List<string> users = new List<string>();

//            users = db.USERS.Where(x => x.username.Contains(term)).Select(y => y.username).ToList();

//            return Json(users, JsonRequestBehavior.AllowGet);
//        }

//        [HttpPost]
//        public JsonResult check_user(string username)
//        {
//            var query = from table in db.USERS
//                        where table.username == username
//                        select table;
//            int count = query.Count();
//            if (count > 0)
//            {
//                return Json(false, JsonRequestBehavior.AllowGet);
//            }
//            return Json(true, JsonRequestBehavior.AllowGet);
//        }

//    }
//}