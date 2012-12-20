using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebMatrix.WebData;
using System.Web.Security;

namespace ITinTheDWebSite.Models
{
    public class AdminModel
    {
        public List<UserInfo> allUsers { get; set; }
        public string[] allRoles { get; set; }
    }

    public class UserInfo
    {
        public ITinTheDWebSite.UserProfile user { get; set; }
        public string[] roles { get; set; }

        public UserInfo(ITinTheDWebSite.UserProfile p)
        {
            var SimpleRoles = (SimpleRoleProvider)Roles.Provider;
            user = p;
            roles = SimpleRoles.GetRolesForUser(p.UserName);
        }
    }

    //public class FileDatabase
    //{
    //    public ITinTheDWebSite.File file { get; set; }

    //    public FileDatabase(ITinTheDWebSite.File f)
    //    {
    //        //var SimpleRoles = (SimpleRoleProvider)Roles.Provider;
    //        file = f;
    //    }
    //}
}