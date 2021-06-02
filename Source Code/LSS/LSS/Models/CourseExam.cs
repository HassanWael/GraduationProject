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

    
    public partial class CourseExam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourseExam()
        {
            this.CourseExamQuestions = new HashSet<CourseExamQuestion>();
        }
    
        public int ID { get; set; }
        public string CourseID { get; set; }
        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public System.DateTime Year { get; set; }
        public string semester { get; set; }
        public string Type { get; set; }
        public string Moderator { get; set; }
        public int ExamWeight { get; set; }
        public string ExamDurationTime { get; set; }
        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public System.DateTime ModerationDate { get; set; }
        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public System.DateTime ExamDate { get; set; }
        public string AdditionalComments { get; set; }
    
        public virtual CourseCoordinator CourseCoordinator { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseExamQuestion> CourseExamQuestions { get; set; }
        public virtual ModerationChecklist ModerationChecklist { get; set; }
    }
}
