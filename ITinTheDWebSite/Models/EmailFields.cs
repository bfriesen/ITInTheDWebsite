using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace sbContact.Models
{
    public class EmailFields
    {

        [Required]
        [StringLength(100)]
        [Display(Order = 1)]
        public virtual string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(254)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        [Display(Order = 2)]
        public virtual string Email { get; set; }

        [Required]
        [StringLength(254)]
        [Display(Order = 3)]
        public virtual string Subject { get; set; }

        [Required]
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        [Display(Order = 4)]
        public virtual string Message { get; set; }
    }
}