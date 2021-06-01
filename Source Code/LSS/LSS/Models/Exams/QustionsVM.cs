using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.Exams
{
    public class QustionsVM
    {
        public QustionsVM(CourseExam CourseExam)
        {
            this.CourseExam = CourseExam;
            Question = new CourseExamQuestion();
            Question.ExamID = CourseExam.ID;

        }
        public   QustionsVM()
        {
            
        }
      
        public CourseExamQuestion Question { get; set; }
        

        public CourseExam CourseExam { get; set; }

        private HashSet<PI> pi { get; set; }
        public HashSet<PI> PIs
        {
            get
            {
                if (pi == null)
                {
                    pi = new HashSet<PI>();
                    foreach (CLO clo in CourseExam.CourseCoordinator.Course.CLOes)
                    {   
                        foreach (PI p in clo.PIs)
                        {
                            pi.Add(p);
                        }
                    }
                }
                return pi;
            }
        }

        public List<PI> _SelectedPI { get; set; }
        public List<PI> SelectedPI { get
            {
                if (_SelectedPI == null)
                {
                    _SelectedPI = new List<PI>();
                    try{
                        _SelectedPI = Question.PIs.ToList();
                    }
                    catch (Exception e )
                    {
                        Console.WriteLine("Error" + e);
                    }
                }
                return _SelectedPI;
            }
        }


    }
}