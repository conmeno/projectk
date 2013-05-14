using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectk.Models;

namespace projectk.Controllers
{
    public class CategoryController : Controller
    {
        private ProjectkContext db = new ProjectkContext();

        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categories);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = db.Categories.Find(id);
            db.Categories.Remove(categories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}