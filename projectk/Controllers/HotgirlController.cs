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
       
        

        public ActionResult Index()
        {
            int EndID = 0;
            List<Article> articles =Variable.GetArticle(ref EndID,Cats.HotGirl);
            ViewBag.EndID = EndID;
            db.SaveChanges();
            return View(articles);
        }
      
        public string load(int id = 0)
        {
            string currentURL = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "") + "/image/";
            int EndID = 0;
            List<Article> articles =Variable.GetArticle(ref EndID,Cats.HotGirl, id);
            return Variable.GenerateArticles(articles, EndID, currentURL);

        } 
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}