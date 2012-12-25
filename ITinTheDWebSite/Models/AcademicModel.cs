using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace ITinTheDWebSite.Models
{
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
}