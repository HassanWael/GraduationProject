using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LSS.Models.CourseFlolder;
using LSS.Models.CourseFlolder.Accriditation;
using LSS.Models.FacultyFolder;
namespace LSS.Models.Forms
{
    public class CourseSyllabusForm
    {
        public Course Course { get; set; }
        public FacultyMember Lecturer { get; set; }
        public List<TextBook> TextBooks { get; set; }
        public List<SLO> SLOs { get; set; }
        public List<SLO> CLOs { get; set; }
        Dictionary<AssessmentMethod, int> AssessmentMethodAndWight { get; set; }
       
    }
}