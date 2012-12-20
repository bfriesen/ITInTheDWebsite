using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace ITinTheDWebSite.Models
{
    public class SponsorModel
    {
        [Key] 
        public int SponsorId { get; set; }

        [Required]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [DisplayName("Company Address")]
        public string CompanyAddress { get; set; }

        [Required]
        [DisplayName("Contact Name")]
        public string ContactName { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Telephone Number")]
        [RegularExpression(@"^\(?([1-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Telephone { get; set; }

        [Required]
        [DisplayName("Email Address")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",ErrorMessage="Please enter a valid email address")]
        public string EmailAddress { get; set; }

        [Required]
        [DisplayName("Briefly explain why you want to participate in IT in the D")]
        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}