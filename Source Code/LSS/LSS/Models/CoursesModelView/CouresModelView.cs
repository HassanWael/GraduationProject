using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CoursesModelView
{
    public class CouresModelView
    {
        public CouresModelView() { }
        /// <summary>
        /// to find all the info needed for the course 
        /// </summary>
        /// <param name="courseCoordinator"></param>
        public CouresModelView(CourseCoordinator courseCoordinator)
        {
            this.CourseCoordinator = courseCoordinator;
        }

        public CourseCoordinator CourseCoordinator { get; set; }
        private Course course { get; set; }
        public Course Course
        {
            get
            {
                if (course == null)
                {
                    course = CourseCoordinator.Course;
                }
                return course;
            }
            set { course = value; }
        }
        private List<OtherLecturer>otherLecturers{get;set;}
        public List<OtherLecturer> OtherLecturers { get
            {
                if (otherLecturers == null)
                {
                    otherLecturers = new List<OtherLecturer>();
                    otherLecturers = CourseCoordinator.OtherLecturers.ToList();
                }

                return otherLecturers;
            }
        }

        private Department department { get; set; }
        public Department Department
        {
            get
            {
                if (department == null)
                {
                    department = Course.Department;
                }
                return department;
            }
            set
            {
                department = value;
            }
        }
        private Faculty faculty { get; set; }
        public Faculty Faculty
        {
            get
            {
                if (faculty == null)
                {
                    faculty = Department.Faculty;
                }
                return faculty;
            }
            set
            {
                faculty = value;
            }
        }
        private List<CLO> clos { get; set; }
        public List<CLO> CLOs
        {
            get
            {
                if (clos == null)
                {
                    clos = Course.CLOes.ToList();
                }
                return clos;
            }
            set
            {
                clos = value;
            }
        }
        private List<CourseTextBook> courseTextBooks { get; set; }
        public List<CourseTextBook> CourseTextBooks
        {
            get
            {
                if (courseTextBooks == null)
                {
                    courseTextBooks = Course.CourseTextBooks.ToList();
                }
                return courseTextBooks;
            }
        }
        private HashSet<PI> pi { get; set; }
        public HashSet<PI> PI
        {
            get
            { //O(n^2)
                if (pi == null)
                {
                    pi = new HashSet<PI>();
                    CLOs.ForEach(item => item.PIs.ToList().ForEach(x => pi.Add(x)));
                }
                return pi;
            }
            set
            {
                pi = value;
            }
        }


        private Dictionary<SLO, HashSet<PI>> _SLO_PI { get; set; }
        public Dictionary<SLO, HashSet<PI>> SLO_PI
        {
            get
            {
                //O(N)
                if (_SLO_PI == null)
                {
                    _SLO_PI = new Dictionary<SLO, HashSet<PI>>();
                    foreach (PI pi in PI)
                    {
                        HashSet<PI> temp = new HashSet<PI>();
                        try
                        {
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("CourseModelView line 122 : " + e.Message);
                        }
                        if (_SLO_PI.ContainsKey(pi.SLO))
                        {
                            temp = _SLO_PI[pi.SLO];
                            temp.Add(pi);
                            _SLO_PI[pi.SLO] = temp;
                        }
                        else
                        {
                            temp.Add(pi);
                            _SLO_PI.Add(pi.SLO, temp);
                        }
                    }
                }

                return _SLO_PI;
            }
            set { _SLO_PI = value; }
        }

        private isAssessed _isAssessed { get; set; }
        public isAssessed IsAssessed
        {
            get
            {
                if (_isAssessed == null)
                {
                    _isAssessed = CourseCoordinator.isAssessed;
                }
                return _isAssessed;
            }
        }

        private AssessmentPlanforTheStudentLearningOutcomeTechnique _AssessmentPlan { get; set; }
        public AssessmentPlanforTheStudentLearningOutcomeTechnique AssessmentPlan
        {
            get
            {
                if (_AssessmentPlan == null)
                {
                    _AssessmentPlan = CourseCoordinator.AssessmentPlanforTheStudentLearningOutcomeTechnique;
                }

                return _AssessmentPlan;
            }
        }
    }
}