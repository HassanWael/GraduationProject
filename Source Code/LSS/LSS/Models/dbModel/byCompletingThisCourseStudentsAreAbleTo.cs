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
    
    public partial class byCompletingThisCourseStudentsAreAbleTo
    {
        public int CoordinatorID { get; set; }
        public string SLO { get; set; }
        public string AggrementLVL { get; set; }
    
        public virtual CourseCoordinator CourseCoordinator { get; set; }
    }
}
