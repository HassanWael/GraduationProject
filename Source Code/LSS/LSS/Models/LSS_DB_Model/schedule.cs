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
    
    public partial class schedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public schedule()
        {
            this.CLOes = new HashSet<CLO>();
        }
    
        public int ID { get; set; }
        public int CoordintaroID { get; set; }
        public int Week { get; set; }
        public string Topics { get; set; }
        public string Assigments { get; set; }
        public string Referance { get; set; }
    
        public virtual CourseCoordinator CourseCoordinator { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLO> CLOes { get; set; }
    }
}
