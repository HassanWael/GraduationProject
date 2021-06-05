using LSS.Models.arc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CoursesModelView
{
    public class HomeCourseListViewModel
    {
        LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        private YearAndSemester YearAndSemester = SemesterSingelton.getCurrentYearAndSemester();
        private List<CourseCoordinator> CCS;

        public HomeCourseListViewModel(String LecturerID)
        {
            Lecturer = _DatabaseEntities.Lecturers.Find(LecturerID);

            CCS = _DatabaseEntities.CourseCoordinators.Where(x => x.YearAndSemester.Year.Equals(YearAndSemester.Year)
                    && x.YearAndSemester.Semester.Equals(YearAndSemester.Semester)).ToList();
        }
        public  Lecturer Lecturer { get; set; }
        private Department department { get; set; }
        public Department Department
        {
            get
            {
                if (department == null)
                {
                    department = Lecturer.Department;
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
        private List< CourseCoordinator> coordinatedCourses { get; set; }

        public List<CourseCoordinator> CoordinatedCourses
        {
            get
            {
                if (coordinatedCourses == null)
                {
                    coordinatedCourses = Lecturer.CourseCoordinators.Where(x => x.Year.Equals(YearAndSemester.Year)&&x.Semester.Equals(YearAndSemester.Semester)).ToList();
                }
                return coordinatedCourses;
            }
            set
            {
                coordinatedCourses = value;
            }
        }

        private List<OtherLecturer> otherCourses { get; set; }

        public List<OtherLecturer> OtherCourses
        {
            get
            {
                if (otherCourses == null)
                {
                    otherCourses = Lecturer.OtherLecturers.Where(x => x.Year.Equals(YearAndSemester.Year)
                                    && x.Semester.Equals(YearAndSemester.Semester)).ToList();
                }
                return otherCourses;
            }
            set
            {
                otherCourses = value;
            }
        }

        private List<CourseCoordinator> departmentCCs { get; set; }
        public List<CourseCoordinator> DepartmentCCs
        {
            get
            {
                if (departmentCCs == null)
                {
                    departmentCCs = _DatabaseEntities.CourseCoordinators.Where(x => x.Course.Department.ID.Equals(Lecturer.dptId)
                                    && x.Year.Equals(YearAndSemester.Year)&& x.Semester.Equals(YearAndSemester.Semester)).ToList();
                }
                return departmentCCs;
            }
        }

        private List<CourseCoordinator> facultyCCS { get; set; }
        public List<CourseCoordinator> FacultyCCS
        {
            get
            {
                if (facultyCCS == null)
                {
                    facultyCCS = _DatabaseEntities.CourseCoordinators.Where(x => x.Course.Department.Faculty.ID.Equals(Lecturer.Department.FacultyId) 
                                && x.Year.Equals(YearAndSemester.Year) && x.Semester.Equals(YearAndSemester.Semester)).ToList();
                }

                return facultyCCS;
            }
        }
    }
}