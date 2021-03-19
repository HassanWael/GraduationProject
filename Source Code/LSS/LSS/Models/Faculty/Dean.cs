using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSS.Models
{
    public class Dean:FacultyMember
    {
        public List<Course> FacultyCourses { get; set; }
        public Faculty Faculty { get; set; }
    }
}
