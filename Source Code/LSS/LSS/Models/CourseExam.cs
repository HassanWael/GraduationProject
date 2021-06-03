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
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CourseExam
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourseExam()
        {
            this.CourseExamQuestions = new HashSet<CourseExamQuestion>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string CourseID { get; set; }
        [Required]
        [Display(Name = "Year")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Year { get; set; }
        [Required]
        public string semester { get; set; }
        [Required]
        [Display(Name = "Exam or Task Name/Type")]
        public string Type { get; set; }
        [Required]
        public string Moderator { get; set; }
        [Required]
        [Display(Name = "Exam weight")]
        public int ExamWeight { get; set; }
        [Required]
        [Display(Name = "Exam Dueration Time")]
        public string ExamDurationTime { get; set; }
        [Display(Name = "Moderation date")]
        [DataType(DataType.Date)]
        public System.DateTime ModerationDate { get; set; }
        [Required]
        [Display(Name = "Exam Date")]
        [DataType(DataType.Date)]
        public System.DateTime ExamDate { get; set; }
        [Display(Name = "Additional Comments")]
        public string AdditionalComments { get; set; }
    
        public virtual CourseCoordinator CourseCoordinator { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseExamQuestion> CourseExamQuestions { get; set; }
        public virtual ModerationChecklist ModerationChecklist { get; set; }
    }
}
