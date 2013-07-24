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
            int numberLoad = Variable.NumberOfArticleLoaded;
            IOAuth1ServiceProvider<IDropbox> dropboxProvider = new DropboxServiceProvider(Variable.ApiKey, Variable.ApiSecret, AccessLevel.Full);

            IDropbox _client = dropboxProvider.GetApi(Variable.UserToken, Variable.UserSecret);

            List<Article> articles = db.Articles.Where(a => a.Cat == (int)Cats.HotGirl).OrderByDescending(a => a.DatePost).Take(numberLoad).Include(a => a.UserProfile).ToList();
            foreach (Article item in articles)
            {
                if (DateTime.Now > item.DropboxShareLinkExpire)
                {
                    var media = _client.GetMediaLinkAsync(item.ExternalURL).Result;
                    item.DropboxShareLink = media.Url;
                    item.DropboxShareLinkExpire = media.ExpireDate;
                }
              
            }

            db.SaveChanges();
            return View(articles);
        }

        //
        // GET: /Hotgirl/Details/5

        public ActionResult Image(int id = 0)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

     
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}