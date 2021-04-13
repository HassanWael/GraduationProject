//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LSS.Models.LSS_DB_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CourseCoordinator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourseCoordinator()
        {
            this.CourseAssessmentMappings = new HashSet<CourseAssessmentMapping>();
            this.CourseTeachingStrategies = new HashSet<CourseTeachingStrategy>();
            this.ModerationChecklists = new HashSet<ModerationChecklist>();
            this.OtherLecturers = new HashSet<OtherLecturer>();
            this.schedules = new HashSet<schedule>();
            this.Student_Course_Grade = new HashSet<Student_Course_Grade>();
            this.AssessmentPlanforTheStudentLearningOutcomeTechniques = new HashSet<AssessmentPlanforTheStudentLearningOutcomeTechnique>();
        }
    
        public string CourseID { get; set; }
        public System.DateTime Year { get; set; }
        public string Semseter { get; set; }
        public string Cordenator { get; set; }
        public int NoOFStudents { get; set; }
        public string ClassRoom { get; set; }
        public string courseNotAssessedReason_ { get; set; }
        public string DayTime { get; set; }
    
        public virtual ActionsForImprovingTheCourse ActionsForImprovingTheCourse { get; set; }
        public virtual byCompletingThisCourseStudentsAreAbleTo byCompletingThisCourseStudentsAreAbleTo { get; set; }
        public virtual Course Course { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseAssessmentMapping> CourseAssessmentMappings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseTeachingStrategy> CourseTeachingStrategies { get; set; }
        public virtual ineffectiveAssessmentStrategi ineffectiveAssessmentStrategi { get; set; }
        public virtual ineffectiveTeachingStrategi ineffectiveTeachingStrategi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModerationChecklist> ModerationChecklists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OtherLecturer> OtherLecturers { get; set; }
        public virtual ResultOfCourseDirectAssessment ResultOfCourseDirectAssessment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<schedule> schedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_Course_Grade> Student_Course_Grade { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessmentPlanforTheStudentLearningOutcomeTechnique> AssessmentPlanforTheStudentLearningOutcomeTechniques { get; set; }
    }
}
