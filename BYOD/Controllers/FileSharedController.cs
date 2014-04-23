using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BYOD.Models;

namespace BYOD.Controllers
{
    [CustomAuthorize]
    public class FileSharedController : Controller
    {
        //
        // GET: /RemoteAccess/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }

        public ActionResult Manager()
        {
            return View();
        }
    }
}
