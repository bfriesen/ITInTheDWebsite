using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ITinTheDWebSite.Models
{
    public class SponsorModel
    {
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
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Please enter a 10 digit number")]
        public string Telephone { get; set; }

        [Required]
        [DisplayName("Email Address")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",ErrorMessage="Please enter a valid email address")]
        public string EmailAddress { get; set; }

        [Required]
        [DisplayName("Briefly explain why you want to participate in IT in the D")]
        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }
    }
}