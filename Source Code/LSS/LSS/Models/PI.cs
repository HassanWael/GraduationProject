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
    
    public partial class PI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PI()
        {
            this.CourseAssessmentMappings = new HashSet<CourseAssessmentMapping>();
            this.CLOes = new HashSet<CLO>();
            this.CourseAssessmentSurvays = new HashSet<CourseAssessmentSurvay>();
        }
        public PI(string SLOID, int deptID)
        : this()
        {
            this.DeptID = deptID;
            this.SLOID = SLOID;
        }
        public string ID { get; set; }
        public string Desc { get; set; }
        public string SLOID { get; set; }
        public int DeptID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseAssessmentMapping> CourseAssessmentMappings { get; set; }
        public virtual SLO SLO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLO> CLOes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseAssessmentSurvay> CourseAssessmentSurvays { get; set; }
    }
}
