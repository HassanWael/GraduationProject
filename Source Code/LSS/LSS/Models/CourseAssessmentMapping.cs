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

    public partial class CourseAssessmentMapping
    {
        public string CourseID { get; set; }
        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public System.DateTime Year { get; set; }
        public string Semseter { get; set; }
        public string AssessmentID { get; set; }
        public int CLO { get; set; }
        public string PI { get; set; }
        public string SLOID { get; set; }
        public int DeptID { get; set; }
    
        public virtual CLO CLO1 { get; set; }
        public virtual CourseCoordinator CourseCoordinator { get; set; }
        public virtual Department Department { get; set; }
    }
}
