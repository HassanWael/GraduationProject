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
    
    public partial class Student_PI_Outcome
    {
        public string CoordinatorID { get; set; }
        public string StudentID { get; set; }
        public int AssessmentMethod { get; set; }
        public Nullable<int> Mark { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual TeachingAndLearningStrategiesTechnique TeachingAndLearningStrategiesTechnique { get; set; }
    }
}
