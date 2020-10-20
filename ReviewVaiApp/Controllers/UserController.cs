using ReviewVaiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ReviewVaiApp.Controllers
{


    public class UserController : Controller
    {
        // GET: User


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult signup(RegisterBindingModel registerBinding)
        {
            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("http://www.reviewbhai.somee.com/api/Account/Register");
                    client.BaseAddress = new Uri("http://localhost:55407/api/Account/Register");

                    var postTask = client.PostAsJsonAsync<RegisterBindingModel>("Register", registerBinding);
                   
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        //return RedirectToAction("/home/index");
                        return Redirect("~/");
                    }
                }

                ModelState.AddModelError(string.Empty, "Server Error");

                //return View();
            }
            return View();

        }


        public ActionResult signin()
        {
            return View();
        }


        public ActionResult logout()
        {

            // Request.GetOwinContext().Authentication.SignOut();
            // return RedirectToAction("signin", "User");
            return View();
        }


    }
}