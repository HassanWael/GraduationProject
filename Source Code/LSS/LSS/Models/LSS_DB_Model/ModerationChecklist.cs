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
    
    public partial class ModerationChecklist
    {
        public int CoordinatorID { get; set; }
        public string Qustion { get; set; }
        public bool Answer { get; set; }
        public string commetns { get; set; }
    
        public virtual CourseCoordinator CourseCoordinator { get; set; }
    }
}
