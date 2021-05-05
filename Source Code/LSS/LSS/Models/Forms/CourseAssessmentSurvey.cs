using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LSS.Models.CourseFlolder;
namespace LSS.Models.Forms
{
    public class CourseAssessmentSurvey
    { 
        public Course Course { set; get; }
        public int Section { get; set; }
        public String year { get; set; }

        Dictionary<String, int> AssessmentQustion = new Dictionary<String, int>();
        public String Suggestions { get; set; }
    }
}