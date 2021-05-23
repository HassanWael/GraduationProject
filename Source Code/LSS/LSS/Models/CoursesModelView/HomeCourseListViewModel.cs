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
        private YearAndSemester YearAndSemester;
        private List<CourseCoordinator> CCS;

        public HomeCourseListViewModel(String LecturerID)
        {
            YearAndSemester = SemesterSingelton.getCurrentYearAndSemester();
            Lecturer = _DatabaseEntities.Lecturers.Find(LecturerID);

            CCS = _DatabaseEntities.CourseCoordinators.Where(x => x.YearAndSemester.Year.Equals(YearAndSemester.Year)&& x.YearAndSemester.Semester.Equals(YearAndSemester.Semester)).ToList();
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
                Department = department;
            }
        }
        private Faculty faculty { get; set; }
        public Faculty Faculty
        {
            get
            {
                if (faculty == null)
                {
                    faculty = department.Faculty;
                }

                return faculty;
            }
        }
        private List< CourseCoordinator> coordinatedCourses { get; set; }

        public List<CourseCoordinator> CoordinatedCourses
        {
            get
            {
                if (coordinatedCourses == null)
                {
                    coordinatedCourses = Lecturer.CourseCoordinators.Where(x => x.YearAndSemester.Equals(YearAndSemester)).ToList();
                }
                return coordinatedCourses;
            }
            set
            {
                coordinatedCourses = value;
            }
        }
        private List<CourseCoordinator> departmentCCs { get; set; }
        public List<CourseCoordinator> DepartmentCCs
        {
            get
            {
                if (departmentCCs == null)
                {
                    departmentCCs = CCS.Where(x => x.Course.dptid.Equals(Department.ID)).ToList();
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
                    facultyCCS = CCS.Where(x => x.Course.Department.FacultyId.Equals(Faculty.ID)).ToList();
                }

                return facultyCCS;
            }
        }
    }
}