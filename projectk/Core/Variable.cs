using projectk.Models;
using Spring.Social.OAuth1;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
 
using System.Data;
using System.Data.Entity;

namespace Projectk
{
    public class Variable
    {
        public static int NumberOfArticleLoaded=10;
        public static bool AutoApprove = true;
        public static string ApiKey = "x9n1ufyyyj00odt";
        public static string ApiSecret = "vwevab5370evd3v";
        public static string UserToken = "uss2v2w38xlfvrb";
        public static string UserSecret = "apz9iqaoi3oa3un";
        [ThreadStatic]
        private static Random rnd;
        public static string WebFolder()
        {
            return HttpRuntime.AppDomainAppPath;
        }
        public static string MapPathFile()
        {
            return HttpRuntime.AppDomainAppPath + "files\\";

        }
        public static int GetRandomInteger()
        {
            if (rnd == null) rnd = new Random((int)DateTime.Now.Ticks);
            return Math.Abs(Convert.ToInt32(rnd.Next(int.MaxValue - 10000) + 10000));

        }

        public static int GetRandomInteger(int minValue, int maxValue)
        {
            if (rnd == null) rnd = new Random((int)DateTime.Now.Ticks);
            return Convert.ToInt32(rnd.Next(minValue, maxValue));
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }



        }
        public static projectk.Models.UserProfile GetUserByID(int id)
        {
            projectk.Models.UserProfile user = null;

            ProjectkContext db = new ProjectkContext();
            user = db.UserProfiles.Find(id);


            return user;
        }

        public static string GenerateArticles(List<Article> articles, int EndID, string currentURL)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Article a in articles)
            {
                sb.Append(GenerateArticle(a, currentURL));
            }
            //render loading button
            if (articles.Count > 0)
            {
                sb.AppendLine("<div class=\"loading\" style=\"display: none;\">");
                sb.AppendLine("        <img src=\"/images/load.png\" />");
                sb.AppendLine("    </div>");
                sb.AppendLine("<div class=\"buttonload\">");
                sb.AppendLine("    <button id=\"load\"  class=\"load btn btn-primary\">load more</button>");
                sb.AppendLine("<input type=\"hidden\" name=\"EndID\" id=\"EndID\" value=\"" + EndID + "\"/>");
                sb.AppendLine("</div>");
            }
            return sb.ToString();
        }
        public static string GenerateArticle(Article a, string currentURL)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("    <div class=\"aitem\">");
            sb.AppendLine("        <div class=\"row-fluid article_item\">");
            sb.AppendLine("            <div class=\"span4\">");
            sb.AppendLine("                <div class=\"aitem-top\">");
            sb.AppendLine("                    <div class=\"aitem-title\">");
            sb.AppendLine("                        <h4><a href=\"image/" + a.ID + "\">" + a.Name + "</a>");
            sb.AppendLine("                        </h4>");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                    <div class=\"aitem-title\">");
            sb.AppendLine("                        <span>Post by</span>&nbsp;<a href=\"#\"><span style=\"font-weight: bold;\">");
            sb.AppendLine(a.UserName);
            sb.AppendLine("                        </span></a>");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                    <div>");
            sb.AppendLine("                        <span>View: " + a.PageView + "</span>&nbsp;<span>Comment: " + a.Comment + "</span>");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                    <div class=\"fb-like\" data-href=\"" + currentURL + "\" data-send=\"false\" data-layout=\"button_count\" data-width=\"50\" data-show-faces=\"true\">");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                </div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <div class=\"span8\">");
            sb.AppendLine("                <div class=\"item-image\">");
            sb.AppendLine("                    <a href=\"image/" + a.ID + "\">");
            sb.AppendLine("                        <img class=\"lazy img-polaroid\"  src=\"/Content/images/loading_anim.gif\" data-original=\"" + a.DropboxShareLink + "\"");
            sb.AppendLine("                    alt=\"" + a.Name + "\" />");
            sb.AppendLine("                    </a>");
            sb.AppendLine("                </div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <div class=\"separate\"></div>");

            return sb.ToString();
        }


        public static string GenerateVideos(List<Article> articles, int EndID, string currentURL)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Article a in articles)
            {
                sb.Append(GenerateVideo(a, currentURL));
            }
            //render loading button
            if (articles.Count > 0)
            {
                sb.AppendLine("<div class=\"loading\" style=\"display: none;\">");
                sb.AppendLine("        <img src=\"/images/load.png\" />");
                sb.AppendLine("    </div>");
                sb.AppendLine("<div class=\"buttonload\">");
                sb.AppendLine("    <button id=\"load\"  class=\"load btn btn-primary\">load more</button>");
                sb.AppendLine("<input type=\"hidden\" name=\"EndID\" id=\"EndID\" value=\"" + EndID + "\"/>");
                sb.AppendLine("</div>");
            }
            return sb.ToString();
        }
        public static string GenerateVideo(Article a, string currentURL)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("    <div class=\"aitem\">");
            sb.AppendLine("        <div class=\"row-fluid article_item\">");
            sb.AppendLine("            <div class=\"span4\">");
            sb.AppendLine("                <div class=\"aitem-top\">");
            sb.AppendLine("                    <div class=\"aitem-title\">");
            sb.AppendLine("                        <h4><a href=\"image/" + a.ID + "\">" + a.Name + "</a>");
            sb.AppendLine("                        </h4>");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                    <div class=\"aitem-title\">");
            sb.AppendLine("                        <span>Post by</span>&nbsp;<a href=\"#\"><span style=\"font-weight: bold;\">");
            sb.AppendLine(a.UserName);
            sb.AppendLine("                        </span></a>");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                    <div>");
            sb.AppendLine("                        <span>View: " + a.PageView + "</span>&nbsp;<span>Comment: " + a.Comment + "</span>");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                    <div class=\"fb-like\" data-href=\"" + currentURL + "\" data-send=\"false\" data-layout=\"button_count\" data-width=\"50\" data-show-faces=\"true\">");
            sb.AppendLine("                    </div>");
            sb.AppendLine("                </div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("            <div class=\"span8\">");
            sb.AppendLine("                <div class=\"item-image\">");

            sb.Append("            <a href=\"/funvideo/"+a.ID+"\"><img alt=\"video\" src=\"http://img.youtube.com/vi/"+a.getYoutubeID()+"/0.jpg\" /></a>");
            sb.Append("            <a style=\"position:absolute;top:150px;left:200px\" href=\"/funvideo/"+a.ID+"\"><img src=\"/images/play.png\" alt=\"video\"/></a>");


            sb.AppendLine("                </div>");
            sb.AppendLine("            </div>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("    <div class=\"separate\"></div>");

            return sb.ToString();
        }


        public static List<Article> GetArticle(ref int EndID, Cats cat, int BeginID = 999999)
        {  
            ProjectkContext db = new ProjectkContext();

            List<Article> articles = new List<Article>();
            int numberLoad = Variable.NumberOfArticleLoaded;
            IOAuth1ServiceProvider<IDropbox> dropboxProvider = new DropboxServiceProvider(Variable.ApiKey, Variable.ApiSecret, AccessLevel.Full);

            IDropbox _client = dropboxProvider.GetApi(Variable.UserToken, Variable.UserSecret);

            articles = db.Articles.Where(a => a.Status==1 && a.Cat == (int)cat && a.ID < BeginID).OrderByDescending(a => a.DatePost).Take(numberLoad).Include(a => a.UserProfile).ToList();
            bool isChange = false;
            foreach (Article item in articles)
            {
                if (DateTime.Now > item.DropboxShareLinkExpire)
                {
                    isChange = true;
                    var media = _client.GetMediaLinkAsync(item.ExternalURL).Result;
                    item.DropboxShareLink = media.Url;
                    item.DropboxShareLinkExpire = media.ExpireDate;
                }
            }
            if (isChange)
                db.SaveChanges();
            if (articles.Count > 0)
                EndID = articles[articles.Count - 1].ID;
            return articles;
        }
        public static List<Article> GetVideo(ref int EndID, int BeginID = 999999)
        {
            ProjectkContext db = new ProjectkContext();

            List<Article> articles = new List<Article>();
            int numberLoad = Variable.NumberOfArticleLoaded;
            
            articles = db.Articles.Where(a => a.Cat == (int)Cats.Video && a.ID < BeginID).OrderByDescending(a => a.DatePost).Take(numberLoad).Include(a => a.UserProfile).ToList();
            
            if (articles.Count > 0)
                EndID = articles[articles.Count - 1].ID;
            return articles;
        }
    }
}