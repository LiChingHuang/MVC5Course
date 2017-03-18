using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View("123");
        }

        public ActionResult View2()
        {
            return PartialView("Index");
        }

        public ActionResult View3()
        {
            return View();
        }

        public ActionResult File1()
        {
            //return File(@"C:\Projects\MVC5Course\MVC5Course\Content\J.jpg", "image/jpeg");
            return File(Server.MapPath("~/Content/J.jpg"), "image/jpeg");
        }

        public ActionResult File2()
        {
            return File(Server.MapPath("~/Content/J.jpg"), "image/jpeg","喬巴.jpg");
        }
    }
}