using ReviewVaiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace ReviewVaiApp.Controllers
{
    public class ReviewGetPostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ReviewGetPost
        public ActionResult Index()
        {
            return View();
        }
        
        
        public ActionResult Reviews()
        {
            return View();
        }

        
        public ActionResult CreateReview()
        {
            return View();
        }

        public ActionResult Reviews1()
        {
            return View();
        }
        public ActionResult showreviews()
        {
            if (User.Identity.IsAuthenticated)
            {
                var ID = User.Identity.GetUserId();
                
            }
            return View();
        }





    }
}