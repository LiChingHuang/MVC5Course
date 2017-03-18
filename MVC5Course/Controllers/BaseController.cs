using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public abstract class BaseController : Controller //抽象類別不能當作action來使用
    {
        public ProductRepository repoProduct = RepositoryHelper.GetProductRepository();

        protected override void HandleUnknownAction(string actionName)
        {
            this.Redirect("/").ExecuteResult(this.ControllerContext);
            //base.HandleUnknownAction(actionName);
        }

        //// GET: Base
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}