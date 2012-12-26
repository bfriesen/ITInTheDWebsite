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

            //var membership = (SimpleMembershipProvider)Membership.Provider;

            //int allUsers = 0;

            //MembershipUserCollection users = membership.GetAllUsers(currentPage, 100, out allUsers);
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
                        TempData["Message"] = "Account successfully deleted.";
                        return RedirectToAction("User", "Admin", new { id = user.FirstOrDefault().UserId });
                    }
                    break;
                case "Student":

                    int x = 1;

                    if(DatabaseHelper.RemoveTranscript(id))
                    {
                        TempData["Message"] += x++ + ". Transcript successfully deleted. ";
                    }

                    if (DatabaseHelper.RemoveFile(id))
                    {
                        TempData["Message"] += x++ + ". File successfully deleted.";
                    }

                    if (DatabaseHelper.RemoveProspectiveData(id))
                    {
                        TempData["Message"] += x++ + ". Account successfully deleted. ";
                    }

                    return RedirectToAction("User", "Admin", new { id = user.FirstOrDefault().UserId });
                    break;
                case "Educator":
                    if (DatabaseHelper.RemoveAcademicData(id))
                    {
                        TempData["Message"] = "Account successfully deleted.";
                        return RedirectToAction("User", "Admin", new { id = user.FirstOrDefault().UserId });
                    }
                    break;
                default:
                    break;
            }

            TempData["Message"] = "Account deletion failed.";
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
    }
}
