using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSS.Models.CourseFlolder.Accriditation;

namespace LSS.Models.CourseFlolder
{
    public class Course
    {
        public String Name { get; set; }
       public List<SLO> CourseSLO { get; set; }
       
    }
}
