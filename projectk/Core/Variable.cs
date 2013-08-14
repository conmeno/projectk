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
        public static bool UseLocalURL = false;
        public static bool UseLocalURLFirstPage = true;
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

        public static string GenerateArticles(List<Article> articles, int EndID, string currentURL, bool UseLocalLink = false)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Article a in articles)
            {
                sb.Append(GenerateArticle(a, currentURL, UseLocalLink));
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
        public static string GenerateArticle(Article a, string currentURL,bool UseLocalLink=false)
        {
            if (UseLocalURL) UseLocalLink = true;
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
            if ((UseLocalURL || UseLocalURLFirstPage)&& UseLocalLink && a.LocalURL != string.Empty && System.IO.File.Exists(HttpContext.Current.Server.MapPath(a.LocalURL)))
            {
                sb.AppendLine("                        <img class=\"lazy img-polaroid\"  src=\"/Content/images/loading_anim.gif\" data-original=\"" + a.LocalURL + "\"");
            }
            else
            {
                sb.AppendLine("                        <img class=\"lazy img-polaroid\"  src=\"/Content/images/loading_anim.gif\" data-original=\"" + a.DropboxShareLink + "\"");
            }

           
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
                sb.AppendLine("        <img src=\"/images/funny_loading.gif\" />");
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

        public static Article GetDropboxShareLink(long id)
        {
            Article item = new Article();

            IOAuth1ServiceProvider<IDropbox> dropboxProvider = new DropboxServiceProvider(Variable.ApiKey, Variable.ApiSecret, AccessLevel.Full);

            IDropbox _client = dropboxProvider.GetApi(Variable.UserToken, Variable.UserSecret);
            ProjectkContext db = new ProjectkContext(); 
             item = db.Articles.Find(id);

            if (item != null)
            {
                var media = _client.GetMediaLinkAsync(item.ExternalURL).Result;
                item.DropboxShareLink = media.Url; 
                item.DropboxShareLinkExpire = media.ExpireDate;
                db.SaveChanges();
            }


            return item;
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
                if (!((Variable.UseLocalURL ||Variable.UseLocalURLFirstPage) && item.LocalURL != string.Empty && System.IO.File.Exists(HttpContext.Current.Server.MapPath(item.LocalURL))))
                {
                    if (DateTime.Now > item.DropboxShareLinkExpire)
                    {
                        isChange = true;
                        var media = _client.GetMediaLinkAsync(item.ExternalURL).Result;
                        item.DropboxShareLink = media.Url;
                        item.DropboxShareLinkExpire = media.ExpireDate;
                    }
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
        public static bool IsMobileClient()
        {
            

            // http://detectmobilebrowsers.com/ (Last update: 03.07.2012)
            string u = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
            System.Text.RegularExpressions.Regex b = new System.Text.RegularExpressions.Regex(@"android.+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|meego.+mobile|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
            System.Text.RegularExpressions.Regex v = new System.Text.RegularExpressions.Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|e\-|e\/|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|xda(\-|2|g)|yas\-|your|zeto|zte\-", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
            if ((b.IsMatch(u) || v.IsMatch(u.Substring(0, 4))))
            {
                return true;
            }
            return false;
        }
    }
}