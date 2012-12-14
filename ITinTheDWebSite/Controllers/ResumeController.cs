using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ITinTheDWebSite.Helpers;

namespace ITinTheDWebSite.Controllers
{
    [Authorize] 
    public class ResumeController : Controller
    {
        //
        // GET: /Resume/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
                                                     // only logged-in user can access this action
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddFile(HttpPostedFileBase UploadFile)
        {
            if (ModelState.IsValid)
            {
                if (UploadFile != null && UploadFile.ContentLength > 0)
                {
                    
                    File f = new File();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        UploadFile.InputStream.CopyTo(ms);

                        f.FileContent = ms.ToArray();
                        f.FileName = Path.GetFileName(UploadFile.FileName);
                        f.ContentType = UploadFile.ContentType;
                        f.ContentLength = UploadFile.ContentLength;

                        DatabaseHelper.UploadFile(f, User.Identity.Name);  // here you write file into the database. You will have to write this method or just add it right here...
          
                    }
                    TempData["Upload"] = "Thank you for uploading your resume";
                    return RedirectToAction("Index", "Resume");

                }
                else
                {
                    ModelState.AddModelError("", "The resume was not uploaded.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }
    }
}
