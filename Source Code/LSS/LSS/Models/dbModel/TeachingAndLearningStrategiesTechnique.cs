//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LSS.Models.dbModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class TeachingAndLearningStrategiesTechnique
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TeachingAndLearningStrategiesTechnique()
        {
            this.Student_PI_Outcome = new HashSet<Student_PI_Outcome>();
        }
    
        public int ID { get; set; }
        public string strategy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_PI_Outcome> Student_PI_Outcome { get; set; }
    }
}
