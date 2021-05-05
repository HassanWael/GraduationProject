using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSS.Models.CourseFlolder;

namespace LSS.Models.FacultyFolder
{
    public class HeadOfDepartment : FacultyMember
    {
        public List<Course> DepartmentCourses { get; set; }
        public  Department Deparment { get; set; }
    }
}
