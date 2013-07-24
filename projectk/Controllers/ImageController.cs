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
    public class ImageController : Controller
    {
        private ProjectkContext db = new ProjectkContext();

        //
        // GET: /Image/

        public ActionResult Index(long id = 0)
        {
            //set pageview
            if (Pageview.A[id] == null)
            {
                Pageview.A[id] = 1;

            }
            else
            {
                Pageview.A[id] = int.Parse(Pageview.A[id].ToString()) + 1;
            }
            //end set pageview
            Article article = db.Articles.Find(id);
            article.UserProfile = db.UserProfiles.Where(a => a.UserName == article.UserName).FirstOrDefault();
            ViewBag.Next = -1;
            ViewBag.Prev = -1;
            Article Next = db.Articles.Where(a => a.ID > id && a.Cat == (int)Cats.Funny).FirstOrDefault();
            if (Next != null)
                ViewBag.Next = Next.ID;
            Article Prev = db.Articles.Where(a => a.ID < id && a.Cat == (int)Cats.Funny).FirstOrDefault();
            if (Prev != null)
                ViewBag.Pre = Prev.ID;
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

    }
}
