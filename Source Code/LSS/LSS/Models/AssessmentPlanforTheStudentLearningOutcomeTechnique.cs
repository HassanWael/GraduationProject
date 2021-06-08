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

    public partial class AssessmentPlanforTheStudentLearningOutcomeTechnique
    {
        [Required]
        [Display(Name = "Course ID")]
        public string CourseID { get; set; }

        [Required]
        public System.DateTime Year { get; set; }

        [Required]
        public string Semester { get; set; }

        [Required]
        [Display(Name = "Midterm Test")]
        public bool Midterm_Test { get; set; }

        [Required]
        [Display(Name = "Final Exam")]
        public bool Final_Exam { get; set; }

        [Required]
        public bool Quiz { get; set; }

        [Required]
        public bool Assignment { get; set; }

        [Required]
        public bool project { get; set; }

        [Required]
        [Display(Name = "Written Report")]
        public bool Written_Report { get; set; }

        [Required]
        [Display(Name = "Oral Presenation")]
        public bool Oral_Presenation { get; set; }

        [Required]
        [Display(Name = "Practice In The Lab")]
        public bool Practice_In_The_Lab { get; set; }

        [Required]
        [Display(Name = "Case Studdy")]
        public bool Case_Studdy { get; set; }

        [Required]
        [Display(Name = "Coruse Assessment Survey By Students")]
        public bool Coruse_Assessment_Survey_By_Students { get; set; }

        [Required]
        [Display(Name = "Group Discussions")]
        public bool Gropu_Discussions { get; set; }

        [Required]
        [Display(Name = "Students Interviews")]
        public bool Students_Interviews { get; set; }

        public string Other { get; set; }
    
        public virtual CourseCoordinator CourseCoordinator { get; set; }
    }
}
