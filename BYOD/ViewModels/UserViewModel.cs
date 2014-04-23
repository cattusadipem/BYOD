using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BYOD.Models;

namespace BYOD.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<DEVICES> Devices { get; set; }
        public IEnumerable<USERS> Users { get; set; }
        public IEnumerable<RULES> Rules { get; set; }
    }
}