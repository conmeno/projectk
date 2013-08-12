using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectk.Models;
using DropNet;
using Projectk;
using Spring.Social.OAuth1;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using System.Web.Security;

namespace projectk.Controllers
{
    public class VideoUploadController : Controller
    {
        private ProjectkContext db = new ProjectkContext();

        //
        // GET: /Article/

        public ActionResult Index()
        {
            //var articles = db.Articles.Include(a => a.UserProfile);
            //return View(articles.ToList());
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

       
        //
        // POST: /Article/Create

        [HttpPost]
        public ActionResult Index(Article article)
        {
            if (ModelState.IsValid)
            { 

                db.Articles.Add(article);
                article.DropboxShareLinkExpire = DateTime.Now;
                //endupload file
                //MembershipUser currentUser;
                //currentUser = Membership.GetUser();

                //article.UserID = currentUser.;


                article.UserID = 1;
                article.UserName = User.Identity.Name;
                article.Cat = (int)Cats.Video;
                article.DatePost = DateTime.Now;
                article.Status = 0;
                if (Variable.AutoApprove)
                    article.Status = 1;
                db.SaveChanges(); 

                return RedirectToAction("Index");
            }

           // ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", article.CategoryID);
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserId", "UserName", article.UserID);
            return View(article);
        }

       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}