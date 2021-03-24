using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSS.Models.CourseFlolder.Accriditation;
using LSS.Models.FacultyFolder;
namespace LSS.Models.CourseFlolder
{
    public class Course
    {
        public String Name { get; set; }
       public List<SLO> CourseSLO { get; set; }
       public Lecturer Lecturer { get; set; }
        public Department Department { get; set; }
    }
}
