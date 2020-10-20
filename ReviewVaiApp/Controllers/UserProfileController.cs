using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace ReviewVaiApp.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
    }
}