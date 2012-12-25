using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace ITinTheDWebSite.Models
{
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

        // Files.

        public HttpPostedFileBase ResumeFile { get; set; }
        public HttpPostedFileBase TranscriptFile { get; set; }
    }
}