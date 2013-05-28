using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projectk.Controllers
{
    public class SuggestionController : Controller
    {
        //
        // GET: /Suggestion/
        [ChildActionOnly]
        public ActionResult Index()
        {
            return PartialView();
        }

    }
}
