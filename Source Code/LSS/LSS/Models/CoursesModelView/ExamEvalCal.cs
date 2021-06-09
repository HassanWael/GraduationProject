using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CoursesModelView
{
    public class ExamEvalCal
    {
        private readonly LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        public ExamEvalCal() { }
        public CourseExam exam { get; set; }
        private List<CourseExamQuestion> questions;
        public int getNoOfStudents(){
            questions = exam.CourseExamQuestions.ToList();
            int a = questions[0].CourseExamEvals.Count();
            foreach(CourseExamQuestion q in questions)
            {
                if(a<q.CourseExamEvals.Count())
                {
                    a = q.CourseExamEvals.Count();
                }
            }
            return a ;
        }

       public  int succsess = 0;
       public int fail = 0;
       
        public double avgGrade()
        {
            double avg = 0; int count = 0;
           List<EnroledStudent> en = exam.CourseCoordinator.EnroledStudents.ToList();
           foreach (EnroledStudent s in en)
            {
                double studentMark = 0;

                foreach (CourseExamEval e in s.CourseExamEvals)
                {
                    studentMark += e.Mark;
                }
                avg += studentMark;
                if (studentMark >= exam.ExamWeight/2)
                {
                    succsess++;
                }
                else
                {
                    fail++;
                }
                count++;
            }
            avg = avg / count;
            return System.Math.Round( avg,2);
        }

       public  double highest = 0;
        public double lowest = 0;
        public double getMedian()
        {
            List<EnroledStudent> en = exam.CourseCoordinator.EnroledStudents.ToList();
            List<double> marks = new List<double>();
            foreach (EnroledStudent s in en)
            {
                double studentMark = 0;

                foreach (CourseExamEval e in s.CourseExamEvals)
                {
                    studentMark += e.Mark;
                }
                marks.Add(studentMark);
            }
            marks.Sort();
            highest = marks[marks.Count() - 1];
            lowest = marks[0];
            if (marks.Count() % 2 == 0)
            { 
                double a = ((marks[marks.Count() / 2 - 1] + marks[marks.Count() / 2]) / 2.0);
                return System.Math.Round(a,2);
            }
            else
            {
               return marks[marks.Count() / 2];
            }
        }

    }
}