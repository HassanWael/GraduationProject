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
    
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            this.AssessedCourses = new HashSet<AssessedCours>();
            this.CLOes = new HashSet<CLO>();
            this.Course1 = new HashSet<Course>();
            this.Course11 = new HashSet<Course>();
            this.CourseCoordinators = new HashSet<CourseCoordinator>();
            this.CourseTeachingStrategies = new HashSet<CourseTeachingStrategy>();
            this.CourseTextBooks = new HashSet<CourseTextBook>();
        }
    
        public string ID { get; set; }
        public string Title { get; set; }
        public string DptID { get; set; }
        public string Pre_requisite { get; set; }
        public string Hours { get; set; }
        public string Co_requisite { get; set; }
        public string Designation { get; set; }
        public string VclassURL { get; set; }
        public string Description { get; set; }
        public string Other_Online_resources { get; set; }
        public string Other_Required_Material { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssessedCours> AssessedCourses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLO> CLOes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> Course1 { get; set; }
        public virtual Course Course2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> Course11 { get; set; }
        public virtual Course Course3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseCoordinator> CourseCoordinators { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseTeachingStrategy> CourseTeachingStrategies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseTextBook> CourseTextBooks { get; set; }
        public virtual Department Department { get; set; }
    }
}
