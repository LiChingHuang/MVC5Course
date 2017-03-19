using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{
    [HandleError(View = "Error_ArgumentException", ExceptionType = typeof(ArgumentException))]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About(string ex="")
        public ActionResult About(int ex) //會發生ArgumentException例外
        {
            ViewBag.Message = "Your application description page!";

            //if (ex == "err")
            //{
            //    throw new ArgumentOutOfRangeException("ex");
            //}

            if (ex == 1)
            {
                throw new Exception("ex");
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Login(string ReturnUrl="") //Get
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM Login, string ReturnUrl="")
        {
            if(ModelState.IsValid)
            {
                FormsAuthentication.RedirectFromLoginPage(Login.Username, false);
                TempData["LoginResult"] = Login;

                if (ReturnUrl.StartsWith("/"))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
                //return Content(Login.Username + ":" + Login.Password);
            }
            //return Content("Login Failed!");
            return View();
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
            //return View();
        }
    }
}