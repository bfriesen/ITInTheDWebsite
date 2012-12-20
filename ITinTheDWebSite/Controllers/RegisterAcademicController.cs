using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITinTheDWebSite.Models;
using ITinTheDWebSite.Helpers;


namespace ITinTheDWebSite.Controllers
{
    public class RegisterAcademicController : Controller
    {
        //
        // GET: /RegisterAcademic/

        public ActionResult Display()
        {
            AcademicModel academic = new AcademicModel();
            DatabaseHelper.GetAcademicdData(academic);
            return View(academic);
        }
        //
        // POST: /RegisterAcademic/Store

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Store(AcademicModel academic)
        {
            if (ModelState.IsValid)
            {
                if (DatabaseHelper.StoreAcademicData(academic))
                {
                    TempData["Message"] = "Thank you for registering to IT in the D. You will be contacted within 24-48 hours.";
                    return RedirectToAction("ThankYou", "Home");
                }
            }
            return RedirectToAction("Display", "RegisterAcademic");
        }

    }
}
