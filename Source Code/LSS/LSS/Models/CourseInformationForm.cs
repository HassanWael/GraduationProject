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
    
    public partial class CourseInformationForm
    {
        public string CourseID { get; set; }
        public System.DateTime Year { get; set; }
        public string Semseter { get; set; }
        public Nullable<bool> CoordinatorSignature { get; set; }
        public Nullable<bool> HeadOFDeptSignature { get; set; }
        public Nullable<bool> ViceDeanSignature { get; set; }
        public string ReasonOfDisaproval { get; set; }
    
        public virtual CourseCoordinator CourseCoordinator { get; set; }
    }
}
