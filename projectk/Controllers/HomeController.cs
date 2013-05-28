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
    public class HomeController : Controller
    {
        private ProjectkContext db = new ProjectkContext();

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
                //if (DateTime.Now > item.DropboxShareLinkExpire)
                {
                    var media = _client.GetMediaLinkAsync(item.ExternalURL).Result;
                    item.DropboxShareLink = media.Url;
                    item.DropboxShareLinkExpire = media.ExpireDate;


                }

            }

            db.SaveChanges();
            return View(articles);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
