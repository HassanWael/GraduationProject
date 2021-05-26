using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.Exams
{
    public class QustionsVM
    {
        public CourseExamQuestion Questions { get; set; }

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

    }
}