using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSS.Models.CourseFlolder;

namespace LSS.Models.FacultyFolder
{
    public class FacultyMember
    {
        public String Name { get; set; }
        public String Password { get; set; }
        public String ImgURL { get; set; }
        public List<Course> Courses { get; set; }
    }
}
