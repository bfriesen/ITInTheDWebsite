using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;
using ITinTheDWebSite.Models;
using ITinTheDWebSite.Helpers;

namespace ITinTheDWebSite.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index(int currentPage = 1)
        {
            var roles = (SimpleRoleProvider)Roles.Provider;
            AdminModel result = new AdminModel
            {
                allUsers = new List<UserInfo>(),
                allRoles = roles.GetAllRoles()
            };

            using (ITintheDTestEntities context = new ITintheDTestEntities())
            {
                var user = from u in context.UserProfiles orderby u.UserId select u;

                foreach (UserProfile p in user)
                {
                    result.allUsers.Add(new UserInfo(p));
                }
            }
            return View(result);
        }

        public ActionResult User(string id)
        {
            ITintheDTestEntities context = new ITintheDTestEntities();

            var user = from u in context.UserProfiles orderby u.UserId select u;
            var roles = (SimpleRoleProvider)Roles.Provider;
            AdminModel result = new AdminModel
            {
                allUsers = new List<UserInfo>(),
                allRoles = roles.GetAllRoles()
            };

            foreach (UserProfile p in user)
            {
                if (p.UserId == Convert.ToInt32(id))
                {
                    result.allUsers.Add(new UserInfo(p));
                }
            }

            return View(result);
        }

        public ActionResult RemoveRole(int id, string role)
        {
            ITintheDTestEntities context = new ITintheDTestEntities();

            var user = from u in context.UserProfiles where u.UserId == id select u;
            var roles = (SimpleRoleProvider)Roles.Provider;

            string[] usrs = new string[] { user.FirstOrDefault().UserName };
            string[] r = new string[] { role };
            if (roles.IsUserInRole(user.FirstOrDefault().UserName, role)) roles.RemoveUsersFromRoles(usrs, r);

            switch (role)
            {
                case "Sponsor":
                    if (DatabaseHelper.RemoveSponsorData(id))
                    {
                        TempData["Message"] = "Role successfully deleted.";
                        return RedirectToAction("User", "Admin", new { id = user.FirstOrDefault().UserId });
                    }
                    break;
                case "Student":

                    int x = 1;

                    if (DatabaseHelper.RemoveTranscript(id))
                    {
                        TempData["Message"] += x++ + ". Transcript successfully deleted. ";
                    }

                    if (DatabaseHelper.RemoveResume(id))
                    {
                        TempData["Message"] += x++ + ". File successfully deleted.";
                    }

                    if (DatabaseHelper.RemoveProspectiveData(id))
                    {
                        TempData["Message"] += x++ + ". Role successfully deleted. ";
                    }

                    return RedirectToAction("User", "Admin", new { id = user.FirstOrDefault().UserId });
                    break;
                case "Educator":
                    if (DatabaseHelper.RemoveAcademicData(id))
                    {
                        TempData["Message"] = "Role successfully deleted.";
                        return RedirectToAction("User", "Admin", new { id = user.FirstOrDefault().UserId });
                    }
                    break;
                case "Admin":
                    if (DatabaseHelper.RemoveAdminData(id))
                    {
                        TempData["Message"] = "Role successfully deleted.";
                        return RedirectToAction("User", "Admin", new { id = user.FirstOrDefault().UserId });
                    }
                    break;
                default:
                    break;
            }

            TempData["Message"] = "Role deletion failed.";
            return RedirectToAction("User", "Admin", new { id = user.FirstOrDefault().UserId });
        }

        public ActionResult AddRole(int id, string role)
        {
            ITintheDTestEntities context = new ITintheDTestEntities();

            var user = from u in context.UserProfiles where u.UserId == id select u;
            var roles = (SimpleRoleProvider)Roles.Provider;

            string[] usrs = new string[] { user.FirstOrDefault().UserName };
            string[] r = new string[] { role };
            if (!roles.IsUserInRole(user.FirstOrDefault().UserName, role)) roles.AddUsersToRoles(usrs, r);

            return RedirectToAction("User", "Admin", new { id = user.FirstOrDefault().UserId });
        }

        public ActionResult DisplayEditRole(int id, string role)
        {
            AcademicModel academic = new AcademicModel();
            if (DatabaseHelper.GetAcademicdData(academic, id) == null)
            {
                TempData["RegistrationMessage"] = "Academic institution registration form.";
            }

            return RedirectToAction("User", "Admin", new { id });
        }

        //
        // GET: /Account/Register

        public ActionResult DisplayAdminRegister()
        {
            RegisterModel adminReg = new RegisterModel();
            if (DatabaseHelper.GetAdminData(adminReg, -1) == null)
            {
                TempData["RegistrationMessage"] = "Admin registration form.";
            }

            return View(adminReg);
        }

        //
        // POST: /Account/Register

        //[Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StoreAdmin(RegisterModel adminReg)
        {
            if (ModelState.IsValid)
            {
                bool edit = false;

                if (adminReg.AccountStatus < 1)
                {
                    adminReg.AccountStatus = 1;
                }

                else if (adminReg.AccountStatus > 3)
                {
                    adminReg.AccountStatus = 3;
                }

                if (DatabaseHelper.StoreAdminData(adminReg, ref edit))
                {
                    int ID = WebSecurity.GetUserId(adminReg.EmailAddress);

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
                        TempData["Message"] = "Successfully registered an Admin";
                        return RedirectToAction("DisplayAdminRegister", "Admin");
                    }
                }

                else
                {
                    TempData["Message"] = "Registeration failed.";
                    return RedirectToAction("DisplayAdminRegister", "Admin");
                }
            }

            // If we got this far, something failed, redisplay form
            TempData["Message"] = "Registeration failed.";
            return RedirectToAction("DisplayAdminRegister", "Admin");
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
