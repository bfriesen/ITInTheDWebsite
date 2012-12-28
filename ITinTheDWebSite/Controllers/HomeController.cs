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

        public ActionResult ThankYou()
        {
            return View();
        }

        public ActionResult DisplayPrivacy()
        {
            return View();
        }

        public ActionResult DisplayTerms()
        {
            return View();
        }
    }
}
