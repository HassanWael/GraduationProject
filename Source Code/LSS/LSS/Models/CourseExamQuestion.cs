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
    
    public partial class CourseExamQuestion
    {
        LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourseExamQuestion()
        {
            this.CourseExamEvals = new HashSet<CourseExamEval>();
            this.CourseCoordinators = new HashSet<CourseCoordinator>();
            this.PIs = new HashSet<PI>();
        }
    
        public int ID { get; set; }
        public int ExamID { get; set; }
        public string QuestionNumber { get; set; }
        public string Question { get; set; }
        public float Weight { get; set; }

        public double getAVG()
        {
            if (CourseExamEvals == null)
            {
                CourseExamEvals = _DatabaseEntities.CourseExamQuestions.Find(ID).CourseExamEvals;
            }

            double sum = 0;

            foreach (CourseExamEval eval in CourseExamEvals)
            {
                sum += eval.Mark;
            }
            try
            {
                double avg = sum / CourseExamEvals.Count;
                avg = System.Math.Round(avg, 2);
                return avg;
            }
            catch
            {
                return 0;
            }
        }

        public virtual CourseExam CourseExam { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseExamEval> CourseExamEvals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseCoordinator> CourseCoordinators { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PI> PIs { get; set; }
    }
}
