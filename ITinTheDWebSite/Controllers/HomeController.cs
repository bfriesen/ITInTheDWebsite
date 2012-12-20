using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITinTheDWebSite.Models;

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
        public ActionResult Application()
        {
            Application app = new Application();
            app.LastName = Request.Form["lastname"];
            return View(app);
        }

        public ActionResult ThankYou()
        {
            return View();
        }

    }
}
