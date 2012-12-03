 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ITinTheDWebSite.Models
{
    public class Application
    {
            [Required]
            [StringLength(100)]
            [Display(Order = 1)]
            public virtual string LastName { get; set; }

        [Required]
            [StringLength(100)]
            [Display(Order = 2)]
            public virtual string FirstName { get; set; }

        [Required]
            [StringLength(1)]
            [Display(Order = 3)]
            public virtual string MiddleInitial { get; set; }

        [Required]
            [StringLength(10)]
            [Display(Order = 4)]
            public virtual string DateOfBirth { get; set; }

        [Required]
            [StringLength(100)]
            [Display(Order = 5)]
            public virtual string EducationLevel { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            [StringLength(254)]
            [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
            [Display(Order = 6)]
            public virtual string Email { get; set; }

            [Required]
            [StringLength(254)]
            [Display(Order = 7)]
            public virtual string Subject { get; set; }

            [Required]
            [StringLength(2000)]
            [DataType(DataType.MultilineText)]
            [Display(Order = 4)]
            public virtual string Message { get; set; }
        }
    
}