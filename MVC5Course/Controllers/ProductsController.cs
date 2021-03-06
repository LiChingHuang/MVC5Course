﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using PagedList;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    
    //[Authorize]
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        //ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        public ActionResult Index(String FilterActive, String sortBy, String keyword, int pageNo=1)
        {
            //靜態選單
            //ViewBag.FilterActive = new SelectList(new List<string> { "True", "False" });

            //動態選單(db)
            var activeOptions = repoProduct.All().Select(p => p.Active.HasValue ? p.Active.Value.ToString() : "False").Distinct().ToList();
            ViewBag.FilterActive = new SelectList(activeOptions);

            doSearchonIndex(sortBy, keyword, pageNo);

            return View();
            //return View(data.Take(10));

            //return View(db.Product.ToList());

            //return View(db.Product.OrderByDescending(p => p.ProductId).Take(10).ToList());
        }

        private void doSearchonIndex(string sortBy, string keyword, int pageNo)
        {
            //var data = db.Product.AsQueryable();
            var all = repoProduct.All().AsQueryable();

            if (!String.IsNullOrEmpty(keyword))
            {
                all = all.Where(p => p.ProductName.Contains(keyword));
            }

            if (sortBy == "+Price")
            {
                all = all.OrderBy(p => p.Price);
            }
            else
            {
                all = all.OrderByDescending(p => p.Price);
            }

            ViewBag.keyword = keyword; //讓View可記得搜尋的字串
            ViewBag.pageNo = pageNo;   //讓View可記得搜尋的字串

            ViewData.Model = all.ToPagedList(pageNo, 10);
            //return View(all.ToPagedList(pageNo, 10)); //分頁
        }

        [HttpPost]
        public ActionResult Index(String FilterActive, Product[] data, String sortBy, String keyword, int pageNo = 1)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var prod = repoProduct.Find(item.ProductId);
                    prod.ProductName = item.ProductName;
                    prod.Price = item.Price;
                    prod.Active = item.Active;
                    prod.Stock = item.Stock;                    
                }

                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            //動態選單(db)
            var activeOptions = repoProduct.All().Select(p => p.Active.HasValue ? p.Active.Value.ToString() : "False").Distinct().ToList();
            ViewBag.FilterActive = new SelectList(activeOptions);

            doSearchonIndex(sortBy, keyword, pageNo);
            return View();
        }
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Product product = db.Product.Find(id);
            Product product = repoProduct.Find(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult ProductOrderLines( int id) //要跟View一樣名字
        {
            Product product = repoProduct.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product.OrderLine);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                //db.SaveChanges();
                repoProduct.Add(product);
                repoProduct.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repoProduct.Find(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(View = "Error_DbEntityValidationException", ExceptionType = typeof(DbEntityValidationException))]
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock,IsDeleted")] Product product)
        public ActionResult Edit(int id, FormCollection form)
        {
            var product = repoProduct.Find(id);
            //if (TryUpdateModel(product)) //Binding有異動的資料
            if (TryUpdateModel(product,new string[] { "ProductName", "Stock","Active" })) //指定欄位 Binding
            {
                //repoProduct.UnitOfWork.Commit();
                //return RedirectToAction("Index");
            }

            //if (ModelState.IsValid)
            //{
            //    var db = repoProduct.UnitOfWork.Context;
            //    db.Entry(product).State = EntityState.Modified;
            //db.SaveChanges();

            //    repoProduct.UnitOfWork.Commit();
            //    return RedirectToAction("Index");
            //}

            //return View(product);

            repoProduct.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repoProduct.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();

            Product product = repoProduct.Find(id);
            repoProduct.Delete(product);
            repoProduct.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }
 //       protected override void Dispose(bool disposing)
 //       {
 //           if (disposing)
 //           {
 //               db.Dispose();
 //           }
 //           base.Dispose(disposing);
 //       }
    }
}
