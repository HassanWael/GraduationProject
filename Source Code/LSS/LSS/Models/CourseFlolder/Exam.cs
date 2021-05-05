using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSS.Models.CourseFlolder
{
    public class Exam:AssessmentMethod
    {
       public  Course Course { get; set; }
        public Exam ID { get; set; }
        public Dictionary<Question, int> Qustions { get; set; }
    }
}
