using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITinTheDWebSite.Models;
using WebMatrix.WebData;
using ITinTheDWebSite.Helpers;

namespace ITinTheDWebSite.Controllers
{   [Authorize]
    public class RegisterProspectController : Controller
    {
        //
        // GET: /RegisterProspect/

        public ActionResult Display()
        {
            ProspectModel prospect = new ProspectModel();
            DatabaseHelper.GetProspectData(prospect);
            return View(prospect);
        }

    
        [ValidateAntiForgeryToken]
        public ActionResult Store(ProspectModel prospect)
        {
            if (ModelState.IsValid)
            {
                if (DatabaseHelper.StoreProspectData(prospect))
                {
                    TempData["Message"] = "Thank you for registering to IT in the D. You will be contacted within 24-48 hours.";
                    return RedirectToAction("ThankYou", "Home");
                }
            }
            return RedirectToAction("Display", "RegisterProspect");
        }
    }

}
