using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CoursesModelView
{
    public class PIAssessmentMV
    {
        private CourseCoordinator cc { get; set; }
        public CourseCoordinator CourseCoordinator { get; set; }
        public IEnumerable<CourseExamQuestion> CourseExamQuestions { get; set; }

        private List<int> _selectedQuestions { get; set; }
        public List<int> SelectedQuestions {
            get{
                
                if (_selectedQuestions == null)
                {
                    _selectedQuestions = new List<int>();
                       _selectedQuestions = CourseCoordinator.CourseExamQuestions.Select(i => i.ID).ToList();
                }
                return _selectedQuestions;
            }
            set
            {
                _selectedQuestions = value;
            }
        }

        private List<int> allQustions { get; set; }
        public  List<int> AllQustions { 
            get 
            {
                if (allQustions == null)
                {
                    allQustions = new List<int>();
                    foreach (CourseExam exam in CourseCoordinator.CourseExams) {
                        foreach (CourseExamQuestion q in exam.CourseExamQuestions)
                        {
                            allQustions.Add(q.ID);
                        }
                    }
                }
                return allQustions;
            }
            set { allQustions = value; }
        }


    }
}