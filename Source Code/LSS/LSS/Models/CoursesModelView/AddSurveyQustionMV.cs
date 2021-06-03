using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CoursesModelView
{
    public class AddSurveyQustionMV
    {
        public CourseCoordinator CC { get; set; }
        
        private CourseAssessmentSurvay _CAS { get; set; }
        public CourseAssessmentSurvay CAS {
            get
            {
                if (_CAS == null)
                {
                    _CAS = new CourseAssessmentSurvay
                    {
                        CourseID = CC.CourseID,
                        Year = CC.Year,
                        Semester = CC.Semester,
                        DeptID = CC.Course.Department.ID
                    };
                }
                return _CAS;
            } 
            set
            {
                _CAS = value;
            }

        }

        private List<CLO> cloes { get; set; }
        public List<CLO> CLOes
        {
            get
            {
                if (cloes == null)
                {
                    cloes =CC.Course.CLOes.ToList();
                }
                return cloes;
            }
        }


        

        private HashSet<PI> pi { get; set; }
        public HashSet<PI> PIs
        {
            get
            {
                if (pi == null)
                {
                    pi = new HashSet<PI>();
                    foreach(CLO clo in CLOes)
                    {
                        foreach( PI p in clo.PIs)
                        {
                            pi.Add(p);
                        }
                    }
                }
                return pi;
            }
        }

        private HashSet<SLO> slo { get; set; }
        public HashSet<SLO> SLO
        {
            get
            {
                if (slo == null)
                {
                    slo = new HashSet<SLO>();
                    foreach (PI pi in PIs)
                    {
                        
                        slo.Add(pi.SLO);
                    }
                }

                return slo;
            }
            set
            {
                slo = value;
            }
        }

        //SLO
        // PI where SLO = SLO


    }
}