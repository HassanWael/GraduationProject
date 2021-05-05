using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CourseFlolder
{
    public class StudentQsutionAnswers
    {
        public Exam Exam { get; set; }
        public  Student Student { get; set; }
        public Question Question { get; set; }
        public String Answer { get; set; }
        public double Grade { get; set; }
    }
}