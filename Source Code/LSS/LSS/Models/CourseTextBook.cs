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
    using System.ComponentModel.DataAnnotations;

    public partial class CourseTextBook
    {
        public string Course { get; set; }
        public string Book_Title { get; set; }
        public string Author { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> YearOfpublish { get; set; }
        public string City_State { get; set; }
        public string Publisher { get; set; }
    
        public virtual Course Course1 { get; set; }
    }
}
