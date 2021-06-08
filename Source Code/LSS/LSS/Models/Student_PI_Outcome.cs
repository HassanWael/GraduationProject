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

    public partial class Student_PI_Outcome
    {
        [Required]
        [Display(Name = "Coordinator ID")]
        public string CoordinatorID { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public string StudentID { get; set; }

        [Required]
        [Display(Name = "Assessment Method")]
        public int AssessmentMethod { get; set; }

        public Nullable<int> Mark { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual TeachingAndLearningStrategiesTechnique TeachingAndLearningStrategiesTechnique { get; set; }
    }
}
