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
    public class ImageUploadController : Controller
    {
        private ProjectkContext db = new ProjectkContext();

        //
        // GET: /Article/

        public ActionResult Index()
        {
            var articles = db.Articles.Where(a => a.UserName == User.Identity.Name).OrderByDescending(a => a.DatePost).Take(100).Include(a => a.UserProfile).ToList();
            return View(articles.ToList());
        }



        //
        // GET: /Article/Create

        public ActionResult Create(int id=0)
        {
            //ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserId", "UserName");
            return View();
        }

        //
        // POST: /Article/Create

        [HttpPost]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                int cat = int.Parse(Request.Form["imageType"].ToString()); ;

                IOAuth1ServiceProvider<IDropbox> dropboxProvider =
         new DropboxServiceProvider(Variable.ApiKey, Variable.ApiSecret, AccessLevel.Full);

                IDropbox _client = dropboxProvider.GetApi(Variable.UserToken, Variable.UserSecret);


                var uniqueID = Variable.GetRandomInteger();



                //upload file
                if (Request.Files.Count == 0 && Request.Files[0].FileName!="")
                    return RedirectToAction("Index");

                HttpPostedFileBase ofile = Request.Files[0];
                string filename = uniqueID + "_" + ofile.FileName;
                if (ofile.ContentLength > 0)
                {
                    string localURL1="";
                    string DropboxURL = "";
                    if (cat == (int)Cats.Funny)
                    {
                        DropboxURL = "/conmeno/funny/" + filename;
                        localURL1 = Variable.WebFolder() + "/Upload/funny/" + filename;
                    }
                    else {
                        localURL1 = Variable.WebFolder() + "/Upload/hotgirl/" + filename;
                        DropboxURL = "/conmeno/hotgirl/" + filename;
                    }

                    ofile.SaveAs(localURL1);
                    var image = System.Drawing.Image.FromFile(localURL1);

                    Spring.IO.FileResource file = new Spring.IO.FileResource(localURL1);

                   
                    if(cat==(int)Cats.Funny) 
                    DropboxURL="/conmeno/funny/" + filename;
                    else
                        DropboxURL = "/conmeno/hotgirl/" + filename;


                    //_client.UploadFileAsync(file, DropboxURL);
                    var upload = _client.UploadFileAsync(file, DropboxURL).Result;

                    //_client.UploadFile("/conmeno/", uniqueID + "_" + ofile.FileName, Variable.ReadFully(ofile.InputStream));
                    article.ExternalURL = DropboxURL;
                    article.LocalURL = localURL1.Replace(Variable.WebFolder(), "");


                    var temp = _client.GetMediaLinkAsync(article.ExternalURL);


                    if (temp != null) article.DropboxShareLink = temp.Result.Url;
                    article.DropboxShareLinkExpire = DateTime.Now.AddHours(3);

                    var a1 = _client.DownloadThumbnailAsync(article.ExternalURL, ThumbnailFormat.Jpeg, ThumbnailSize.Medium).Result;
                    article.ThumbnailData = a1.Content;


                }

                //endupload file

                //article.UserID = currentUser.;


                article.UserID = 1;
                article.UserName = User.Identity.Name;
                if (cat == (int)Cats.Funny) 
                article.Cat = (int)Cats.Funny;
                else
                    article.Cat = (int)Cats.HotGirl;
                article.Status = 0;
                if (Variable.AutoApprove)
                    article.Status = 1;
                article.DatePost = DateTime.Now;
                db.Articles.Add(article);
                db.SaveChanges(); 


                return RedirectToAction("Index");
            }

            // ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", article.CategoryID);
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserId", "UserName", article.UserID);
            return View(article);
        }

        //
        // GET: /Article/Create

        public ActionResult Check(int type=1)
        {
            Cats temp = (Cats)type;
            var articles = db.Articles.Where(a => a.Cat == (int)temp).OrderByDescending(a => a.DatePost).Take(100).Include(a => a.UserProfile).ToList();
            
            return View(articles.ToList());
        }


        //
        // GET: /Article/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // POST: /Article/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("check");
        }


        public ActionResult Approve(long id)
        {
            Article article = db.Articles.Find(id);
            article.Status = 1; 
            db.SaveChanges();
            return RedirectToAction("Check");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}