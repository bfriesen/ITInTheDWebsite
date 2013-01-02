using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using ITinTheDWebSite.Models;
using ITinTheDWebSite.Helpers;

namespace ITinTheDWebSite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //  POST: Student Information.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult StoreProspect(ProspectModel prospect)
        {
            if (ModelState.IsValid)
            {
                bool edit = false;

                if (prospect.AccountStatus < 1)
                {
                    prospect.AccountStatus = 1;
                }

                else if (prospect.AccountStatus > 4)
                {
                    prospect.AccountStatus = 4;
                }

                if (prospect.ProspectiveStudentTextField == null)
                {
                    ProspectModel oldProspect = new ProspectModel();

                    string field = DatabaseHelper.GetProspectData(oldProspect, WebSecurity.GetUserId(prospect.EmailAddress)).ProspectiveStudentTextField;

                    prospect.ProspectiveStudentTextField = field;
                }

                if (DatabaseHelper.StoreProspectData(prospect, ref edit))
                {
                    int ID = WebSecurity.GetUserId(prospect.EmailAddress);

                    if (edit == true && ID != WebSecurity.CurrentUserId)
                    {
                        TempData["Message"] = "Successfully edited the user's information.";

                        return RedirectToAction("User", "Admin", new { ID });
                    }

                    else if (edit == true)
                    {
                        TempData["Message"] = "Successfully edited your information.";
                        return RedirectToAction("Manage", "Account");
                    }

                    else
                    {
                        TempData["Message"] = "Thank you for registering to IT in the D. You will be contacted within 24-48 hours.";
                        return RedirectToAction("ThankYou", "Home");
                    }
                }

                else
                {
                    TempData["Message"] = "Registeration failed.";
                    return RedirectToAction("DisplayProspect", "Account");
                }
            }

            TempData["Message"] = "Registeration failed.";
            return RedirectToAction("DisplayProspect", "Account");
        }

        // GET: Student Information.

        [AllowAnonymous]
        public ActionResult DisplayProspect()
        {
            ProspectModel prospect = new ProspectModel();
            if (DatabaseHelper.GetProspectData(prospect, -1) == null)
            {
                TempData["RegistrationMessage"] = "Prospective student registration form.";
            }

            return View(prospect);
        }

        // POST: Resume and/or Transcript.

        public ActionResult StoreProspectFiles(ProspectModel prospect)
        {
            prospect = DatabaseHelper.GetProspectData(prospect, -1);

            var resumeUploaded = DatabaseHelper.GetProspectData(prospect, -1).ResumeFile;

            bool edit = false;
            if (resumeUploaded == null || resumeUploaded.ContentLength == 0)
            {
                TempData["Message"] = "Required files are not uploaded, please upload required files and try again.";
                return RedirectToAction("DisplayProspectFiles", "Account");
            }

            else
            {
                if (DatabaseHelper.StoreProspectData(prospect, ref edit))
                {
                    if (DatabaseHelper.GetProspectData(prospect, WebSecurity.GetUserId(prospect.EmailAddress)).ResumeUploaded == "No")
                    {
                        TempData["Message"] = "Uploading files failed.";
                        return RedirectToAction("DisplayProspectFiles", "Account");
                    }

                    TempData["Message"] = "Successfully uploaded your files.";
                    return RedirectToAction("DisplayProspectFiles", "Account");
                }

                else
                {
                    TempData["Message"] = "Uploading files failed.";
                    return RedirectToAction("DisplayProspectFiles", "Account");
                }
            }
        }

        // GET: Resume and/or Transcript.

        [Authorize(Roles = "Student")]
        public ActionResult DisplayProspectFiles()
        {
            ProspectModel prospect = new ProspectModel();
            prospect = DatabaseHelper.GetProspectData(prospect, -1);

            return View(prospect);
        }

        // POST: Dynamic Prospect page.

        public ActionResult StoreProspectPage(ProspectModel prospect)
        {
            bool edit = false;

            if (prospect.ProspectiveStudentTextField == null)
            {
                prospect = DatabaseHelper.GetProspectData(prospect, WebSecurity.GetUserId(User.Identity.Name));

                prospect.ProspectiveStudentTextField = "";

                if (DatabaseHelper.StoreProspectData(prospect, ref edit))
                {
                    TempData["Message"] = "Successfully deleted your story as a graduate student.";
                    return RedirectToAction("Index", "AcademicInstitution");
                }

                else
                {
                    TempData["Message"] = "Failed to delete your story as a graduate student.";
                    return RedirectToAction("Index", "AcademicInstitution");
                }
            }

            else
            {
                ProspectModel currentProspect = new ProspectModel();

                currentProspect = DatabaseHelper.GetProspectData(currentProspect, WebSecurity.GetUserId(User.Identity.Name));

                string newTextField = prospect.ProspectiveStudentTextField;

                prospect = currentProspect;

                prospect.ProspectiveStudentTextField = newTextField;

                if (DatabaseHelper.StoreProspectData(prospect, ref edit))
                {
                    TempData["Message"] = "Successfully entered your story as a graduate student.";
                    return RedirectToAction("Index", "GraduateStudents");
                }

                else
                {
                    TempData["Message"] = "Failed to enter your story as a graduate student.";
                    return RedirectToAction("Index", "GraduateStudents");
                }
            }            
        }

        // GET: Dynamic Prospect page.

        public ActionResult DisplayProspectPage()
        {
            ProspectModel currentProspect = new ProspectModel();

            currentProspect = DatabaseHelper.GetProspectData(currentProspect, WebSecurity.GetUserId(User.Identity.Name));

            return View(currentProspect);
        }

        // POST: /RegisterAcademic/Store

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult StoreAcademic(AcademicModel academic)
        {
            if (ModelState.IsValid)
            {
                bool edit = false;

                if (academic.AccountStatus < 1)
                {
                    academic.AccountStatus = 1;
                }

                else if (academic.AccountStatus > 3)
                {
                    academic.AccountStatus = 3;
                }

                if (academic.AcademicInstitutionTextField == null)
                {
                    AcademicModel oldAcademic = new AcademicModel();

                    string field = DatabaseHelper.GetAcademicdData(oldAcademic, WebSecurity.GetUserId(academic.PrimaryEmailAddress)).AcademicInstitutionTextField;

                    academic.AcademicInstitutionTextField = field;
                }

                if (DatabaseHelper.StoreAcademicData(academic, ref edit))
                {
                    int ID = WebSecurity.GetUserId(academic.PrimaryEmailAddress);

                    if (edit == true && ID != WebSecurity.CurrentUserId)
                    {
                        TempData["Message"] = "Successfully edited the user's information.";

                        return RedirectToAction("User", "Admin", new { ID });
                    }

                    if (edit == true)
                    {
                        TempData["Message"] = "Successfully edited your information.";
                        return RedirectToAction("Manage", "Account");
                    }

                    else
                    {
                        TempData["Message"] = "Thank you for registering to IT in the D. You will be contacted within 24-48 hours.";
                        return RedirectToAction("ThankYou", "Home");
                    }
                }

                else
                {
                    TempData["Message"] = "Registeration failed.";
                    return RedirectToAction("DisplayAcademic", "Home");
                }
            }

            TempData["Message"] = "Registeration failed.";
            return RedirectToAction("DisplayAcademic", "Home");
        }
        
        // GET: /RegisterAcademic/Display

        [AllowAnonymous]
        public ActionResult DisplayAcademic()
        {
            AcademicModel academic = new AcademicModel();
            if (DatabaseHelper.GetAcademicdData(academic, -1) == null)
            {
                TempData["RegistrationMessage"] = "Academic institution registration form.";
            }

            return View(academic);
        }

        // POST: Dynamic Academic page.

        public ActionResult StoreAcademicPage(AcademicModel academic)
        {
            bool edit = false;

            if (academic.AcademicInstitutionTextField == null)
            {
                academic = DatabaseHelper.GetAcademicdData(academic, WebSecurity.GetUserId(User.Identity.Name));

                academic.AcademicInstitutionTextField = "";

                if (DatabaseHelper.StoreAcademicData(academic, ref edit))
                {
                    TempData["Message"] = "Successfully deleted your information as an academic institution.";
                    return RedirectToAction("Index", "AcademicInstitution");
                }

                else
                {
                    TempData["Message"] = "Failed to delete your information as an academic institution.";
                    return RedirectToAction("Index", "AcademicInstitution");
                }
            }

            else
            {
                AcademicModel currentAcademic = new AcademicModel();

                currentAcademic = DatabaseHelper.GetAcademicdData(currentAcademic, WebSecurity.GetUserId(User.Identity.Name));

                string newTextField = academic.AcademicInstitutionTextField;

                academic = currentAcademic;

                academic.AcademicInstitutionTextField = newTextField;

                if (DatabaseHelper.StoreAcademicData(academic, ref edit))
                {
                    TempData["Message"] = "Successfully entered your information as an academic institution.";
                    return RedirectToAction("Index", "AcademicInstitution");
                }

                else
                {
                    TempData["Message"] = "Failed to enter your information as an academic institution.";
                    return RedirectToAction("Index", "AcademicInstitution");
                }
            }
        }

        // GET: Dynamic Academic page.

        public ActionResult DisplayAcademicPage()
        {
            AcademicModel currentAcademic = new AcademicModel();

            currentAcademic = DatabaseHelper.GetAcademicdData(currentAcademic, WebSecurity.GetUserId(User.Identity.Name));

            return View(currentAcademic);
        }

        // POST: /RegisterSponsor/Store

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult StoreSponsor(SponsorModel sponsor)
        {
            if (ModelState.IsValid)
            {
                bool edit = false;

                if (sponsor.AccountStatus < 1)
                {
                    sponsor.AccountStatus = 1;
                }

                else if (sponsor.AccountStatus > 3)
                {
                    sponsor.AccountStatus = 3;
                }

                if (sponsor.SponsorTextField == null)
                {
                    SponsorModel oldSponsor = new SponsorModel();

                    string field = DatabaseHelper.GetSponsorData(oldSponsor, WebSecurity.GetUserId(sponsor.EmailAddress)).SponsorTextField;

                    sponsor.SponsorTextField = field;
                }

                if (DatabaseHelper.StoreSponsorData(sponsor, ref edit))
                {
                    int ID = WebSecurity.GetUserId(sponsor.EmailAddress);

                    if (edit == true && ID != WebSecurity.CurrentUserId)
                    {
                        TempData["Message"] = "Successfully edited the user's information.";

                        return RedirectToAction("User", "Admin", new { ID });
                    }

                    if (edit == true)
                    {
                        TempData["Message"] = "Successfully edited your information.";
                        return RedirectToAction("Manage", "Account");
                    }

                    else
                    {
                        TempData["Message"] = "Thank you for registering to IT in the D. You will be contacted within 24-48 hours.";
                        return RedirectToAction("ThankYou", "Home");
                    }
                }

                else
                {
                    TempData["Message"] = "Registeration failed.";
                    return RedirectToAction("DisplaySponsor", "Account");
                }
            }

            TempData["Message"] = "Registeration failed.";
            return RedirectToAction("DisplaySponsor", "Account");
        }

        // GET: /RegisterSponsor/Display

        [AllowAnonymous]
        public ActionResult DisplaySponsor()
        {
            SponsorModel spons = new SponsorModel();
            if (DatabaseHelper.GetSponsorData(spons, -1) == null)
            {
                TempData["RegistrationMessage"] = "Sponsor registration form.";
            }

            return View(spons);

        }

        // POST: Dynamic Academic page.

        public ActionResult StoreSponsorPage(SponsorModel sponsor)
        {
            bool edit = false;

            if (sponsor.SponsorTextField == null)
            {
                sponsor = DatabaseHelper.GetSponsorData(sponsor, WebSecurity.GetUserId(User.Identity.Name));

                sponsor.SponsorTextField = "";

                if (DatabaseHelper.StoreSponsorData(sponsor, ref edit))
                {
                    TempData["Message"] = "Successfully deleted your information as a sponsor.";
                    return RedirectToAction("Index", "Sponsors");
                }

                else
                {
                    TempData["Message"] = "Failed to delete your information as a sponsor.";
                    return RedirectToAction("Index", "Sponsors");
                }
            }

            else
            {
                SponsorModel currentSponsor = new SponsorModel();

                currentSponsor = DatabaseHelper.GetSponsorData(currentSponsor, WebSecurity.GetUserId(User.Identity.Name));

                string newTextField = sponsor.SponsorTextField;

                sponsor = currentSponsor;

                sponsor.SponsorTextField = newTextField;

                if (DatabaseHelper.StoreSponsorData(sponsor, ref edit))
                {
                    TempData["Message"] = "Successfully entered your information as a sponsor.";
                    return RedirectToAction("Index", "Sponsors");
                }

                else
                {
                    TempData["Message"] = "Failed to enter your information as a sponsor.";
                    return RedirectToAction("Index", "Sponsors");
                }
            }
        }

        // GET: Dynamic Academic page.

        public ActionResult DisplaySponsorPage()
        {
            SponsorModel currentSponsor = new SponsorModel();

            currentSponsor = DatabaseHelper.GetSponsorData(currentSponsor, WebSecurity.GetUserId(User.Identity.Name));

            return View(currentSponsor);
        }

        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }
        }

        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (UsersContext db = new UsersContext())
                {
                    Models.UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new Models.UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        #endregion
    }


}
