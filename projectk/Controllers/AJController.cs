using projectk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projectk.Controllers
{
    public class AJController : Controller
    {
        //
        // GET: /AJ/


        public string UpdateLike()
        {
            string link = "";
            if (Request.QueryString["link"] != null)
            {
                link = Request.QueryString["link"];
                updateTodb(link);
            }
            return "like server " + link;
        }
        public string UpdateUnLike()
        {
            string link = "";
            if (Request.QueryString["link"] != null)
            {
                link = Request.QueryString["link"];
                updateTodb(link, false);
            }
            return "unlike server " + link;
        }
        public void updateTodb(string link, bool isLike = true)
        {
            string aid = "";
            int id = 0;
            var temp = link.Split('/');
            if (temp.Count() > 1)
                aid = temp[temp.Count() - 1];

            int.TryParse(aid, out id);
            if (id > 0)
            {
                //update like for this id;
                Article a;
                ProjectkContext db = new ProjectkContext();
                a = db.Articles.Find(id);
                if (isLike)
                {
                    if (a != null)
                    {
                        a.Like += 1;
                        db.SaveChanges();
                    }
                }
                else
                {

                    if (a != null)
                    {
                        a.Like -= 1;
                        db.SaveChanges();
                    }
                }


            }
        }
    }
}
