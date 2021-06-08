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

    public partial class CourseReport
    {
        [Required]
        [Display(Name = "Course ID")]
        public string CourseID { get; set; }

        [Required]
        public System.DateTime Year { get; set; }

        [Required]
        public string Semester { get; set; }

        [Display(Name = "Coordinator Signature")]
        public Nullable<bool> CoordinatorSignature { get; set; }

        [Display(Name = "Head OF Dept Signature")]
        public Nullable<bool> HeadOFDeptSignature { get; set; }

        [Display(Name = "Vice Dean Signature")]
        public Nullable<bool> ViceDeanSignature { get; set; }

        [Display(Name = "Reason Of Disaproval")]
        public string ReasonOfDisaproval { get; set; }
    
        public virtual CourseCoordinator CourseCoordinator { get; set; }
    }
}
