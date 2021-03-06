﻿using projectk.Models;
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
            List<Article> listTopVideo = db.Articles.Where(a => a.Cat == (int)Cats.Video).OrderByDescending(a => a.DatePost).Take(5).ToList();
            List<Article> listTopFunny = db.Articles.Where(a => a.Cat == (int)Cats.Funny).OrderByDescending(a => a.DatePost).Take(5).ToList();
            ViewBag.listTopView = listTopVideo;
            ViewBag.listTopFunny = listTopFunny;
             
            return PartialView();
        }


        [ChildActionOnly]
        public ActionResult MostViewed()
        {
            List<Article> articles = db.Articles.OrderByDescending(a => a.PageView).Take(5).ToList();
            ViewBag.articles = articles; 
            return PartialView();
        }


        [ChildActionOnly]
        public ActionResult OlderFunny(int ID)
        {
            List<Article> articles = db.Articles.Where(a => a.Cat == (int)Cats.Funny && a.ID < ID).OrderByDescending(a => a.DatePost).Take(8).ToList();
            ViewBag.articles = articles;
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult OlderVideo(int ID)
        {
            List<Article> articles = db.Articles.Where(a => a.Cat == (int)Cats.Video && a.ID < ID).Take(8).ToList();
            ViewBag.articles = articles;
            return PartialView();
        }


    }
}
