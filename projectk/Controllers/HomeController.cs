using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectk.Models; 
using Projectk;

namespace projectk.Controllers
{
    public class HomeController : Controller
    {
        private ProjectkContext db = new ProjectkContext();

        public ActionResult Index()
        {
           // Redirect("/funny");

            return RedirectToAction("Index","Funny");
        }
 
    }
}
