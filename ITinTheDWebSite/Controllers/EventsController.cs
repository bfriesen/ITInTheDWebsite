using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace ITinTheDWebSite.Controllers
{
    public class EventsController : Controller
    {
        //
        // GET: /Events/

        public ActionResult Index ()
        {
            return View(ITinTheDWebSite.Models.RssReader.GetRssFeed());
        }
    }
}
