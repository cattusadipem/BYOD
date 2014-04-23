﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BYOD.Models;
using BYOD.Models;
//using BYOD.ViewModels;

namespace BYOD.Controllers
{
    [CustomAuthorize]
    public class RulesController : Controller
    {
        //
        // GET: /Rules/
        private BYODEntities db = new BYODEntities();
        //private VIEWAPP app = new VIEWAPP();

        public ActionResult Index()
        {
            //var rule = new RULES();
            //rule.RULES_APP = new List<RULES_APP>();
            //PopulateAssignedCourseData(rule);
            //return View();
            var rule = db.RULES.ToList();
            return View(rule);
        }


        //Get: /Application
        public ActionResult AddRule()
        {
            var app = db.APPLICATIONS.ToList();
            //var instructor = new APPLICATIONS();
            //instructor.RULES_APP= new List<RULES_APP>();
            //PopulateAssignedCourseData(instructor);
            return View(app);

        }

        //private void PopulateAssignedCourseData(APPLICATIONS app)
        //{
        //    var allapp = db.APPLICATIONS;
        //    var instructorCourses = new HashSet<int>(app.RULES_APP.Select(c => c.app_id));
        //    var app = new HashSet<int>(app.
        //    var viewModel = new List<VIEWAPP>();
        //    foreach (var course in allapp)
        //    {
        //        viewModel.Add(new VIEWAPP
        //        {
        //            app_id = course.app_id,
        //            name = course.name,
        //            Assigned = instructorCourses.Contains(course.app_id)
        //        });
        //    }
        //    ViewBag.Courses = viewModel;
        //}
        //POST: /Rules/AddRule
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRule([Bind(Include = "name,allow_copy_paste,allow_email,allow_camera,allow_microphone,allow_airplan_mode,allow_screen_capture,allow_clipboard,allow_home_key,dowload_type,allow_share")]RULES rule, [Bind(Include = "rules_id,app_id,status")] RULES_APP rule_app, APPLICATIONS app)
        {
            
          try
            {
                if (ModelState.IsValid)
                {
                    db.RULES.Add(rule);
                    db.SaveChanges();
                    //rule_app.rules_id = db.RULES.Where(x => x.rules_id.Equals(rule.rules_id)).Select(y => y.rules_id).FirstOrDefault();
                    rule_app.app_id = db.APPLICATIONS.Where(a => a.app_id.Equals(app.app_id)).Select(b => b.app_id).SingleOrDefault();
                    //rule_app.app_id = Convert.ToInt32(actions);
                    db.RULES_APP.Add(rule_app);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(rule);
        }
        // add rule
    //[HttpPost] 
    //public ActionResult AddRule(RULES rule) 
    //{ 
    //  try 
    //  {
    //      db.RULES.Add(rule); 
    //      db.SaveChanges(); 
    //      return RedirectToAction("Index"); 
    //  }
    //  catch (Exception)
    //  {
    //      ModelState.AddModelError("", "Unable to save changes. An error has occurred.");
    //  }
    //  return View(); 
    //}

       // POST: /Rules/Delete/?
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Delete(int id)
                {
                    try
                    {
                        RULES rule = db.RULES.Find(id);
                        db.RULES.Remove(rule);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Unable to save changes. An error has occurred.");
                        return RedirectToAction("Delete", new { id = id, saveChangesError = true });
                    }
                    return RedirectToAction("Index");
                }
                // GET: /LoaiSP/Edit/5

                public ActionResult Edit(int id = 0)
                {
                    RULES rule = db.RULES.Find(id);
                    if (rule == null)
                    {
                        return HttpNotFound();
                    }
                    return View(rule);
                }

    }
}
