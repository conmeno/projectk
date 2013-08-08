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
    public class FunnyController : Controller
    {
        private ProjectkContext db = new ProjectkContext();

        //
        // GET: /Hai/

        public ActionResult Index()
        {
            //int numberLoad = Variable.NumberOfArticleLoaded;
            //IOAuth1ServiceProvider<IDropbox> dropboxProvider =new DropboxServiceProvider(Variable.ApiKey, Variable.ApiSecret, AccessLevel.Full);

            //IDropbox _client = dropboxProvider.GetApi(Variable.UserToken, Variable.UserSecret);

            List<Article> articles = GetArticle();// db.Articles.Where(a => a.Cat == (int)Cats.Funny).OrderByDescending(a => a.DatePost).Take(numberLoad).Include(a => a.UserProfile).ToList();
            //foreach (Article item in articles)
            //{
            //    if (DateTime.Now > item.DropboxShareLinkExpire)
            //    {
            //        var media = _client.GetMediaLinkAsync(item.ExternalURL).Result;
            //        item.DropboxShareLink = media.Url;
            //        item.DropboxShareLinkExpire = media.ExpireDate;
            //    } 
            //}

            db.SaveChanges();
            return View(articles);
        }
        public List<Article> GetArticle(int BeginID=0)
        {
            List<Article> articles = new List<Article>();
            int numberLoad = Variable.NumberOfArticleLoaded;
            IOAuth1ServiceProvider<IDropbox> dropboxProvider = new DropboxServiceProvider(Variable.ApiKey, Variable.ApiSecret, AccessLevel.Full);

            IDropbox _client = dropboxProvider.GetApi(Variable.UserToken, Variable.UserSecret);

             articles = db.Articles.Where(a => a.Cat == (int)Cats.Funny && a.ID>BeginID).OrderByDescending(a => a.DatePost).Take(numberLoad).Include(a => a.UserProfile).ToList();
            foreach (Article item in articles)
            {
                if (DateTime.Now > item.DropboxShareLinkExpire)
                {
                    var media = _client.GetMediaLinkAsync(item.ExternalURL).Result;
                    item.DropboxShareLink = media.Url;
                    item.DropboxShareLinkExpire = media.ExpireDate;
                }
            }
            return articles;
        }
        public string load(int id=0)
        {
            List<Article> articles = GetArticle(id);
            return GenerateArticles(articles);

        }

        public string GenerateArticles(List<Article> articles)
        { System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Article a in articles)
            {
                sb.Append(GenerateArticle(a));
            }
            return sb.ToString();
        }
        public string GenerateArticle(Article a)
        {
            string currentURL = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("    <div class=\"aitem\">");
            sb.AppendLine("        <div class=\"row-fluid article_item\">");
            sb.AppendLine("            <div class=\"span4\">");
            sb.AppendLine("                <div class=\"aitem-top\">");
            sb.AppendLine("                    <div class=\"aitem-title\">");
            sb.AppendLine("                        <h4><a href=\"funny/image/"+a.ID+"\">"+a.Name+"</a>");
            sb.AppendLine("                        </h4>");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                    <div class=\"aitem-title\">");
            sb.AppendLine("                        <span>Post by</span>&nbsp;<a href=\"#\"><span style=\"font-weight: bold;\">");
            sb.AppendLine(a.UserName);
            sb.AppendLine("                        </span></a>");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                    <div>");
            sb.AppendLine("                        <span>View: "+a.PageView+"</span>&nbsp;<span>Comment: "+a.Comment+"</span>");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                    <div class=\"fb-like\" data-href=\"" + currentURL + "\" data-send=\"false\" data-layout=\"button_count\" data-width=\"50\" data-show-faces=\"true\">");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                </div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <div class=\"span8\">");
            sb.AppendLine("                <div class=\"item-image\">");
            sb.AppendLine("                    <a href=\"/funny/image/"+a.ID+"\">");
            sb.AppendLine("                        <img class=\"lazy img-polaroid\"  src=\"~/Content/images/loading_anim.gif\" data-original=\""+a.DropboxShareLink+"\"");
            sb.AppendLine("                    alt=\""+a.Name+"\" />");
            sb.AppendLine("                    </a>");
            sb.AppendLine("                </div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <div class=\"separate\"></div>");

            return sb.ToString();
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