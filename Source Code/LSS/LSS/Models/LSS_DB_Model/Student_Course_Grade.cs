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
    
    public partial class Student_Course_Grade
    {
        public string CourseID { get; set; }
        public System.DateTime Year { get; set; }
        public string Semseter { get; set; }
        public string StudentID { get; set; }
        public Nullable<int> Mark { get; set; }
    
        public virtual CourseCoordinator CourseCoordinator { get; set; }
        public virtual Student Student { get; set; }
    }
}