using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITinTheDWebSite.Models;
using System.IO;


namespace ITinTheDWebSite.Controllers
{
    public class Apply_OnlineController : Controller
    {
        //
        // GET: /Apply Online/

        public ActionResult Index()
        {
            Application app = new Application();
            return View(app);
        }



    }
}
