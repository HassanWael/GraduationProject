using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LSS.Models.CourseFlolder;
namespace LSS.Models.Forms
{
    public class ExamEvaluationForm
    {
        public Course Course { get; set; }
        public StudentQsutionAnswers studentQsutionAnswers { get; set; }
        public int noOfEnrolledStudens;
        public int noOfAttendees;
        public int noOfAbsebbtees;
        public double avargeGrade;
        public double meadianGrade;
        public double sucessRate;
        public double highestGrade;
        public double lowestGrade;
        public double examAbsenceRate;
        public DateTime date { get; set; }
    }    
}