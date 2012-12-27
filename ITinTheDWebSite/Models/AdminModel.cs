using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebMatrix.WebData;
using System.Web.Security;
using System.Web.Mvc;
using DataAnnotationsExtensions;

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

    // Admin Register Model.

    public class RegisterModel
    {
        // Required fields.

        [Required]
        [Display(Name = "Name *")]
        public string Name { get; set; }

        [Required]
        [Email]
        [Display(Name = "Email Address *")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password *")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password *")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // Unrequired fields.

        [DisplayName("Telephone Number")]
        [RegularExpression(@"^\(?([1-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Telephone { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }
    }
}