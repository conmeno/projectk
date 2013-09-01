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
using System.Drawing;
using System.IO;

namespace projectk.Controllers
{
    public class UpdateLinkController : Controller
    {
        private ProjectkContext db = new ProjectkContext();

        //
        // GET: /UpdateLink/

        public ActionResult Index()
        {

            //IOAuth1ServiceProvider<IDropbox> dropboxProvider =
            //     new DropboxServiceProvider(Variable.ApiKey, Variable.ApiSecret, AccessLevel.Full);

            //IDropbox _client = dropboxProvider.GetApi(Variable.UserToken, Variable.UserSecret);
            //List<Article> articles = db.Articles.Include(a => a.UserProfile).Where(a=>a.Cat==(int)Cats.Funny).ToList();
            //foreach (Article item in articles)
            //{
            //    //if (item.DropboxShareLinkExpire != null )
            //    //{
            //    //    int a = 3;
            //    //}
            //    //if (DateTime.Now > item.DropboxShareLinkExpire)
            //    //{
            //        var media = _client.GetMediaLinkAsync(item.ExternalURL).Result;
            //        item.DropboxShareLink = media.Url;
            //        item.DropboxShareLinkExpire = DateTime.Now.AddHours(3);// media.ExpireDate;


            //        //update thumbnail
            //        var a1 = _client.DownloadThumbnailAsync(item.ExternalURL, ThumbnailFormat.Jpeg, ThumbnailSize.Medium).Result;
            //        item.ThumbnailData = a1.Content;
            //    //}

            //}
            //ViewBag.message = "success";
            //db.SaveChanges();
            return View();
        }

        public ActionResult GenerateThumbnail()
        {

            IOAuth1ServiceProvider<IDropbox> dropboxProvider =
                 new DropboxServiceProvider(Variable.ApiKey, Variable.ApiSecret, AccessLevel.Full);

            IDropbox _client = dropboxProvider.GetApi(Variable.UserToken, Variable.UserSecret);
            List<Article> articles = db.Articles.Include(a => a.UserProfile).ToList();
            int aa = 300;
            foreach (Article item in articles)
            {

                //var media = _client.GetMediaLinkAsync(item.ExternalURL).Result;
                //item.DropboxShareLink = media.Url;
                //item.DropboxShareLinkExpire = DateTime.Now.AddHours(3);// media.ExpireDate;


                //update thumbnail
                if (item.Cat == (int)Cats.Funny || item.Cat == (int)Cats.HotGirl)
                {
                    var a1 = _client.DownloadThumbnailAsync(item.ExternalURL, ThumbnailFormat.Jpeg, ThumbnailSize.Medium).Result;
                    if (a1.Content.Count() > 0)
                    {
                        string fileName = "upload/";
                        if (item.Cat == (int)Cats.Funny)
                            fileName += "funny/";
                        else
                            fileName += "hotgirl/";
                        string ThumbnailURL = Variable.WebFolder() + fileName + "thumbnail/" + DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss.ffff") +".jpg";
                        MemoryStream ms = new MemoryStream(a1.Content);
                        Image returnImage = Image.FromStream(ms);
                        if (returnImage != null)
                        {
                            returnImage.Save(ThumbnailURL);
                            item.ThumbnailURL ="/"+ ThumbnailURL.Replace(Variable.WebFolder(), "");
                        }
                    }
                }


            }
            
            ViewBag.message = "success";
            db.SaveChanges();
            return View();
        }

    }
}
