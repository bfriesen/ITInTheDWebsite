using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using DataAnnotationsExtensions;
using System.ComponentModel;
using System.Web;

namespace ITinTheDWebSite.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    [Table("UserProfile")]
    public class LoginModel
    {
        [Required]
        [Email]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class AcademicModel
    {
        //[Key]
        //public int AcademicId { get; set; }

        // Required fields.
        
        [Required]
        [DisplayName("Academic Institution *")]
        public string AcademyName { get; set; }

        [Required]
        [DisplayName("Primary Contact Name *")]
        public string PrimaryContactName { get; set; }

        [Required]
        [DisplayName("Email Address *")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter a valid email address")]
        public string PrimaryEmailAddress { get; set; }

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

        // Unrequired Fields.

        [DisplayName("Address")]
        public string AcademyAddress { get; set; }

        [DisplayName("Title")]
        public string PrimaryTitle { get; set; }

        [DisplayName("Primary Telephone Number")]
        [RegularExpression(@"^\(?([1-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string PrimaryTelephone { get; set; }

        [DisplayName("Secondary Contact Name")]
        public string SecondaryContactName { get; set; }

        [DisplayName("Secondary Title")]
        public string SecondaryTitle { get; set; }

        [DisplayName("Secondary Telephone Number")]
        [RegularExpression(@"^\(?([1-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string SecondaryTelephone { get; set; }

        [DisplayName("Secondary Email Address")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter a valid email address")]
        public string SecondaryEmailAddress { get; set; }
    }

    public class ProspectModel
    {
        // Required fields.

        [Required]
        [DisplayName("Name *")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Email Address *")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter a valid email address")]
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

        [DisplayName("Desired Career Path")]
        public string DesiredCareerPath { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        public string TranscriptUploaded { get; set; }

        public string ResumeUploaded { get; set; }

        // Files.

        public HttpPostedFileBase ResumeFile { get; set; }
        public HttpPostedFileBase TranscriptFile { get; set; }
    }

    public class SponsorModel
    {
        [Key]
        public int SponsorId { get; set; }

        // Required fields.

        [Required]
        [DisplayName("Company Name *")]
        public string CompanyName { get; set; }

        [Required]
        [DisplayName("Contact Name *")]
        public string ContactName { get; set; }

        [Required]
        [DisplayName("Email Address *")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter a valid email address.")]
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

        [DisplayName("Company Address")]
        public string CompanyAddress { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Telephone Number")]
        [RegularExpression(@"^\(?([1-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Telephone { get; set; }

        [DisplayName("Briefly explain why you want to participate in IT in the D")]
        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
