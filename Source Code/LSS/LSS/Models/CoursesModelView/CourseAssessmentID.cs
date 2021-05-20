using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CoursesModelView
{
    public class CourseAssessmentID
    {
        public CourseCoordinator CC { get; set; }

        private IEnumerable<CourseAssessmentSurvay> _AssessmentQustions { get; set; }
        public IEnumerable<CourseAssessmentSurvay> AssessmentQustions { get
            {
                if (_AssessmentQustions == null)
                {
                    _AssessmentQustions = CC.CourseAssessmentSurvays;
                }
                return _AssessmentQustions;
            }
            set
            {
                _AssessmentQustions = value;
            }
        }

        private Dictionary<CourseAssessmentSurvay, List<AssessmentSurveyAnswer>> _AnswerList { get; set; }

        public Dictionary<CourseAssessmentSurvay, List<AssessmentSurveyAnswer>> AnswerList { get
            {
                if (_AnswerList == null)
                {
                    _AnswerList = new Dictionary<CourseAssessmentSurvay, List<AssessmentSurveyAnswer>>();
                    foreach (CourseAssessmentSurvay s in AssessmentQustions)
                        if (s.AssessmentSurveyAnswers != null) { 
                        _AnswerList.Add(s, s.AssessmentSurveyAnswers.ToList());
                        }
                }
                return _AnswerList; 
            }
            set
            {
                _AnswerList = value;
            }
        }

        public double getScore(int QID)
        {
            CourseAssessmentSurvay s = AssessmentQustions.Where(x => x.ID.Equals(QID)).FirstOrDefault();
            int   sum = 0;
            int count = 0;
            foreach(AssessmentSurveyAnswer answer in AnswerList[s])
            {
                try
                {
                    sum =int.Parse( answer.Answer);
                    count++;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Errorr at Line 47 of CourseAssessmentID");
                }
            }
            double avg = sum / count * 1.0;
            avg = Math.Floor(avg * 100) / 100;
            return avg;
        }
    }


}