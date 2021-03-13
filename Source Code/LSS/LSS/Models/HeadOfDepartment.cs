using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSS.Models
{
    public class HeadOfDepartment : FacultyMember
    {
        public List<Course> DepartmentCourses { get; set; }
        public  Deparment Deparment { get; set; }
    }
}
