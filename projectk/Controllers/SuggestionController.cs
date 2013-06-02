using projectk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projectk.Controllers
{
    public class SuggestionController : Controller
    {
        private ProjectkContext db = new ProjectkContext();
        //
        // GET: /Suggestion/
        [ChildActionOnly]
        public ActionResult Index()
        {
            List<Article> listTopVideo = db.Articles.Where(a => a.Cat == (int)Cats.Video).Take(5).ToList();
            List<Article> listTopFunny = db.Articles.Where(a => a.Cat == (int)Cats.Funny).Take(5).ToList();
            ViewBag.listTopView = listTopVideo;
            ViewBag.listTopFunny = listTopFunny;
             
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult OlderFunny(int ID)
        {
            List<Article> articles = db.Articles.Where(a => a.Cat == (int)Cats.Funny && a.ID>ID).Take(5).ToList();
            ViewBag.articles = articles;
            return PartialView();
        }


    }
}
