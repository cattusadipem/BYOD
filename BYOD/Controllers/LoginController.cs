using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace BYOD.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Account/Login

        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        public ActionResult Index(FormCollection collection, string returnUrl)
        {

            string username = collection["username"];
            string password = collection["password"];

            if (username == "admin" && password == "admin123")
            {
                Session["User"] = "admin";
                FormsAuthentication.SetAuthCookie(username, true);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            // If we got this far, something failed, redisplay form
            else
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View();
        }

        //
        // POST: /Account/LogOff

        public ActionResult Logout()
        {
            Session["User"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

    }
}
