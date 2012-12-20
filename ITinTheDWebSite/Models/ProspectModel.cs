using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ITinTheDWebSite.Models
{
    public class ProspectModel
    {
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Telephone Number")]
        [RegularExpression(@"^\(?([1-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string Telephone { get; set; }

        [Required]
        [DisplayName("Email Address")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; } 

        [Required]
        [DisplayName("Desired Career Path")]
        public string DesiredCareerPath { get; set; }

        [Required]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        public HttpPostedFileBase ResumeFile { get; set; }
        public HttpPostedFileBase TranscriptFile { get; set; }

    }
}