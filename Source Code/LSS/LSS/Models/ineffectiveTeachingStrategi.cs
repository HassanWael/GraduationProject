//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LSS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ineffectiveTeachingStrategi
    {
        public string CourseID { get; set; }
        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public System.DateTime Year { get; set; }
        public string Semseter { get; set; }
        public string Reason { get; set; }
    
        public virtual CourseCoordinator CourseCoordinator { get; set; }
    }
}
