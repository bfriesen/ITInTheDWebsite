using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ITinTheDWebSite.Models
{
    public class AcademicModel
    {
        //[Key]
        //public int AcademicId { get; set; }

        [Required]
        [DisplayName("Academic Institution")]
        public string AcademyName { get; set; }

        [Required]
        [DisplayName("Address")]
        public string AcademyAddress { get; set; }

        [Required]
        [DisplayName("Primary Contact Name")]
        public string PrimaryContactName { get; set; }

        [Required]
        [DisplayName("Title")]
        public string PrimaryTitle { get; set; }

        [Required]
        [RegularExpression(@"^\(?([1-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string PrimaryTelephone { get; set; }

        [Required]
        [DisplayName("Email Address")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter a valid email address")]
        public string PrimaryEmailAddress { get; set; }

        [Required]
        [DisplayName("Secondary Contact Name")]
        public string SecondaryContactName { get; set; }

        [Required]
        [DisplayName("Title")]
        public string SecondaryTitle { get; set; }

        [Required]
        [RegularExpression(@"^\(?([1-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string SecondaryTelephone { get; set; }

        [Required]
        [DisplayName("Email Address")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter a valid email address")]
        public string SecondaryEmailAddress { get; set; }
    }
}