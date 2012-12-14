using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITinTheDWebSite.Helpers;
using ITinTheDWebSite.Models;

namespace ITinTheDWebSite.Controllers
{   [Authorize]
    public class RegisterSponsorController : Controller
    {
        private ITintheDTestEntities1 db = new ITintheDTestEntities1();
        [HttpGet]
        // GET: /RegisterSponsor

        public ActionResult Display()
        {
            SponsorModel spons = new SponsorModel();
            int id = DatabaseHelper.GetIDfromUserName(User.Identity.Name);

            CorporateSponsor corporatesponsor = db.CorporateSponsors.SingleOrDefault(c => c.SponsorID == id);
            if (corporatesponsor != null)
            {
                spons.CompanyName = corporatesponsor.CompanyName;
                spons.CompanyAddress = corporatesponsor.CompanyAddress;
                spons.ContactName = corporatesponsor.ContactName;
                spons.Title = corporatesponsor.Title;
                spons.Telephone = corporatesponsor.Telephone;
                spons.EmailAddress = corporatesponsor.EmailAddress;
                spons.Reason = corporatesponsor.Reason;
            }    
            return View(spons);
            
           
        }

        //
        // POST: /RegisterSponsor/Store

        [HttpPost]
        public ActionResult Store(SponsorModel sponsor)
        {
            if (ModelState.IsValid)
            {
                bool add = false;
                int id = DatabaseHelper.GetIDfromUserName(User.Identity.Name);
                
                CorporateSponsor corporatesponsor = db.CorporateSponsors.SingleOrDefault(c => c.SponsorID == id);
                if (corporatesponsor == null)
                {
                    corporatesponsor = new CorporateSponsor();
                    add = true;
                }
                corporatesponsor.SponsorID = id;
                corporatesponsor.CompanyAddress = sponsor.CompanyAddress;
                corporatesponsor.CompanyName = sponsor.CompanyName;
                corporatesponsor.ContactName = sponsor.ContactName;
                corporatesponsor.EmailAddress = sponsor.EmailAddress;
                corporatesponsor.Title = sponsor.Title;
                corporatesponsor.Telephone = sponsor.Telephone;
                corporatesponsor.Reason = sponsor.Reason;
                if (add == true)
                {
                    db.CorporateSponsors.AddObject(corporatesponsor);
                }
                
                db.SaveChanges();
                return View();

            }
            return RedirectToAction("Display");
        }


    }
}