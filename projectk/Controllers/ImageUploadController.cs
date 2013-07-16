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
            var articles = db.Articles.Include(a => a.UserProfile);
            return View(articles.ToList());
        }

       

        //
        // GET: /Article/Create

        public ActionResult Create()
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
                
                IOAuth1ServiceProvider<IDropbox> dropboxProvider =
         new DropboxServiceProvider(Variable.ApiKey,Variable.ApiSecret, AccessLevel.Full);

                IDropbox _client = dropboxProvider.GetApi(Variable.UserToken,Variable.UserSecret); 

                
                var uniqueID = Variable.GetRandomInteger();



                //upload file
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase ofile = Request.Files[0];
                    string filename = uniqueID + "_" + ofile.FileName;
                    if (ofile.ContentLength > 0)                    
                    {
                         
                        ofile.SaveAs( Variable.WebFolder()+"/Upload/" + filename);
                        var image=System.Drawing.Image.FromFile(Variable.WebFolder() + "/Upload/" + filename);
                        
                        Spring.IO.FileResource file = new Spring.IO.FileResource(Variable.WebFolder() + "/Upload/" + filename);
                        
                        string DropboxURL = "/conmeno/" + filename;


                        //_client.UploadFileAsync(file, DropboxURL);
                        var upload = _client.UploadFileAsync(file, DropboxURL).Result;

                        //_client.UploadFile("/conmeno/", uniqueID + "_" + ofile.FileName, Variable.ReadFully(ofile.InputStream));
                        article.ExternalURL = DropboxURL;



                        var temp = _client.GetMediaLinkAsync(article.ExternalURL); 


                        if (temp != null) article.DropboxShareLink = temp.Result.Url;
                        article.DropboxShareLinkExpire = DateTime.Now.AddHours(3);

                        var a1 = _client.DownloadThumbnailAsync(article.ExternalURL, ThumbnailFormat.Jpeg, ThumbnailSize.Medium).Result;
                        article.ThumbnailData = a1.Content;
                   
                          
                    }
                }
                //endupload file
              
                //article.UserID = currentUser.;


                article.UserID = 1;
                article.UserName = User.Identity.Name;
                article.Cat = (int)Cats.Funny;
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

        public ActionResult Check()
        {
            var articles = db.Articles.Include(a => a.UserProfile);
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
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}