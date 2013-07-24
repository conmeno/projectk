﻿using System;
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
    public class FunnyController : Controller
    {
        private ProjectkContext db = new ProjectkContext();

        //
        // GET: /Hai/

        public ActionResult Index()
        {
            int numberLoad = Variable.NumberOfArticleLoaded;
            IOAuth1ServiceProvider<IDropbox> dropboxProvider =new DropboxServiceProvider(Variable.ApiKey, Variable.ApiSecret, AccessLevel.Full);

            IDropbox _client = dropboxProvider.GetApi(Variable.UserToken, Variable.UserSecret);

            List<Article> articles = db.Articles.Where(a => a.Cat == (int)Cats.Funny).OrderByDescending(a => a.DatePost).Take(numberLoad).Include(a => a.UserProfile).ToList();
            foreach (Article item in articles)
            {
                if (DateTime.Now > item.DropboxShareLinkExpire)
                {
                    var media = _client.GetMediaLinkAsync(item.ExternalURL).Result;
                    item.DropboxShareLink = media.Url;
                    item.DropboxShareLinkExpire = media.ExpireDate;
                }
                //if (item.UserName == null || item.UserName == string.Empty)
                //{
                //    UserProfile u = Variable.GetUserByID(item.UserID);
                //    if (u == null)
                //        item.UserName = "";
                //    else
                //        item.UserName = u.UserName;
                //}
            }

            db.SaveChanges();
            return View(articles);
        }

        //
        // GET: /Hai/Details/5

        public ActionResult Image(long id = 0)
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
        public void FindNext(DbSet<Article> a, long currentID)
        {

        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}