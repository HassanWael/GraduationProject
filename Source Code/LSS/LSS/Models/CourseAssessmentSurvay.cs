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
    
    public partial class CourseAssessmentSurvay
    {
        public string CourseID { get; set; }
        public System.DateTime Year { get; set; }
        public string Semseter { get; set; }
        public string Qustion { get; set; }
    
        public virtual CourseCoordinator CourseCoordinator { get; set; }
    }
}
