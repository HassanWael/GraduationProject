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
    
    public partial class ActionsForImprovingTheCourse
    {
        public string CourseID { get; set; }
        public System.DateTime Year { get; set; }
        public string Semseter { get; set; }
        public string ActionsTaken { get; set; }
        public string ActionsResults { get; set; }
        public string RecomandedActions { get; set; }
        public string PersonResponsible { get; set; }
    
        public virtual CourseCoordinator CourseCoordinator { get; set; }
    }
}
