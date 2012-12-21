using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;
using System.Web.Security;

namespace ITinTheDWebSite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {            
            SeedMembership();
            AreaRegistration.RegisterAllAreas();


            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }


        private void SeedMembership()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin")) roles.CreateRole("Admin");
            if (!roles.RoleExists("Student")) roles.CreateRole("Student");
            if (!roles.RoleExists("Sponsor")) roles.CreateRole("Sponsor");
            if (!roles.RoleExists("Educator")) roles.CreateRole("Educator");



           // int test = 0;
          // var result = membership.GetAllUsers(1, 5, out test);

            ///* make sure user miticv is admin user! */
            //if (!roles.RoleExists("Admin"))
            //{
            //    roles.CreateRole("Admin");
            //}
            //if (membership.GetUser("miticv", false) == null)
            //{
            //    membership.CreateUserAndAccount("miticv", "password");
            //}
            //if (!roles.GetRolesForUser("miticv").Contains("Admin"))
            //{
            //    roles.AddUsersToRoles(new[] { "miticv" }, new[] { "admin" });
            //}
        }
    }

}
