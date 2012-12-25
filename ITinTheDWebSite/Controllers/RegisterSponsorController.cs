using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITinTheDWebSite.Helpers;
using ITinTheDWebSite.Models;
using WebMatrix.WebData;

namespace ITinTheDWebSite.Controllers
{
    public class RegisterSponsorController : Controller
    {
        //private ITintheDTestEntities1 db = new ITintheDTestEntities1();
        [HttpGet]
        // GET: /RegisterSponsor

        public ActionResult Display()
        {
            SponsorModel spons = new SponsorModel();
            DatabaseHelper.GetSponsorData(spons);
            return View(spons);

        }

        //
        // POST: /RegisterSponsor/Store

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Store(SponsorModel sponsor)
        {
            if (ModelState.IsValid)
            {
                if (DatabaseHelper.StoreSponsorData(sponsor))
                {
                    TempData["Message"] = "Thank you for registering to IT in the D. You will be contacted within 24-48 hours.";
                    return RedirectToAction("ThankYou", "Home");
                }

                else
                {
                    TempData["Message"] = "Registeration failed.";
                    return RedirectToAction("Display", "RegisterSponsor");
                }
            }
            return RedirectToAction("Display", "RegisterSponsor");
        }


    }
}