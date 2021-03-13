using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSS.Models
{
    public class FacultyMember
    {
        public String Name { get; set; }
        public String Password { get; set; }
        public String ImgURL { get; set; }
        public List<Course> Courses { get; set; }
    }
}
