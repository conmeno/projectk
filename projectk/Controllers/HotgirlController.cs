using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectk.Models;
using Spring.Social.OAuth1;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using Projectk;
namespace projectk.Controllers
{
    public class HotgirlController : Controller
    {
        private ProjectkContext db = new ProjectkContext();
       
        //
        // GET: /Hotgirl/

        public ActionResult Index()
        {
            IOAuth1ServiceProvider<IDropbox> dropboxProvider =
       new DropboxServiceProvider(Variable.ApiKey, Variable.ApiSecret, AccessLevel.Full);

            IDropbox _client = dropboxProvider.GetApi(Variable.UserToken, Variable.UserSecret);



            List<Article> articles = db.Articles.Include(a => a.Category).Include(a => a.UserProfile).ToList();
            foreach (Article item in articles)
            {
                //if (item.DropboxShareLinkExpire != null )
                //{
                //    int a = 3;
                //}
                if (DateTime.Now > item.DropboxShareLinkExpire)
                {
                    var media = _client.GetMediaLinkAsync(item.ExternalURL).Result;
                    item.DropboxShareLink = media.Url;
                    item.DropboxShareLinkExpire = DateTime.Now.AddDays(1);// media.ExpireDate;


                }

            }

            db.SaveChanges();
            return View(articles);
        }

        //
        // GET: /Hotgirl/Details/5

        public ActionResult Details(int id = 0)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // GET: /Hotgirl/Create

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Hotgirl/Create

        [HttpPost]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", article.CategoryID);
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserId", "UserName", article.UserID);
            return View(article);
        }

        //
        // GET: /Hotgirl/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", article.CategoryID);
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserId", "UserName", article.UserID);
            return View(article);
        }

        //
        // POST: /Hotgirl/Edit/5

        [HttpPost]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", article.CategoryID);
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserId", "UserName", article.UserID);
            return View(article);
        }

        //
        // GET: /Hotgirl/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // POST: /Hotgirl/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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