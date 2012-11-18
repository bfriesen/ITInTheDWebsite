using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITinTheDWebSite.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Homepage/

        public ActionResult Index()
        {
            return View();
        }

    }
}
