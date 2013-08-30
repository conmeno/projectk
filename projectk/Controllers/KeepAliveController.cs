using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projectk.Controllers
{
    public class KeepAliveController : Controller
    {
        //
        // GET: /KeepAlive/

        public ActionResult Index()
        {
            return View();
        }
        public string load()
        {
            return DateTime.Now.ToString();
        }

    }
}
