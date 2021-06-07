using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSS.Models;
using LSS.Models.arc;
using OfficeOpenXml;
using PagedList;

namespace LSS.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        readonly LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        // GET: Admin
        readonly YearAndSemester YAS = SemesterSingelton.getCurrentYearAndSemester();
        public ActionResult Index(string? message)
        {
            if (message == null)
            {
                ViewBag.message = message;
            }
            else
            {
                ViewBag.message = "";

            }
            return View();
        }

        public ActionResult AddCourseToSemester(string? CourseID)
        {
            if (CourseID == null)
            {
                return RedirectToAction("ListCourses", "Admin");
            }
            CourseCoordinator cc;
            if (_DatabaseEntities.CourseCoordinators.Find(CourseID, YAS.Year, YAS.Semester) == null)
            {
                cc = new CourseCoordinator
                {
                    CourseID = CourseID,
                    Year = YAS.Year,
                    Semester = YAS.Semester
                };
            }
            else
            {
                cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, YAS.Year, YAS.Semester);
            }
            Course course = _DatabaseEntities.Courses.Find(CourseID);
            int FacultyID = course.Department.FacultyId;
            List<Lecturer> lec = _DatabaseEntities.Lecturers.Where(x => x.Department.FacultyId.Equals(FacultyID)).ToList();

            ViewBag.Lecturers = new SelectList(lec, "ID", "Name");
            return View(cc);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourseToSemester(CourseCoordinator cc)
        {
            Course course = _DatabaseEntities.Courses.Find(cc.CourseID);
            int FacultyID = course.Department.FacultyId;
            List<Lecturer> lec = _DatabaseEntities.Lecturers.Where(x => x.Department.FacultyId.Equals(FacultyID)).ToList();
            ViewBag.Lecturers = new SelectList(lec, "ID", "Name");

            if (ModelState.IsValid)
            {
                string message = "Course";

                try
                {
                    CourseCoordinator courseC = _DatabaseEntities.CourseCoordinators.Find(cc.CourseID, cc.Year, cc.Semester);

                    if (courseC == null)
                    {
                        _DatabaseEntities.CourseCoordinators.Add(cc);
                        _DatabaseEntities.SaveChanges();
                        message = "Course Added successfuly ";
                    }
                    else
                    {
                        courseC.Coordinator = cc.Coordinator;
                        courseC.DayTime = cc.DayTime;
                        courseC.ClassRoom = cc.ClassRoom;

                        _DatabaseEntities.Entry(courseC).State = EntityState.Modified;
                        _DatabaseEntities.SaveChanges();
                        message = "Course Edited successfuly ";

                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Year", " An errorr Has Acoured please try again later");
                    Console.WriteLine("Error at the Line 48 of AdminController : " + e.Message);
                    return View();
                }

                return RedirectToAction("ListCourses", "Admin", new { message });
            }
            return View();
        }

        public ActionResult CreateCourse()
        {
            ViewBag.dept  = _DatabaseEntities.Departments.ToList();
            ViewBag.courses = _DatabaseEntities.Courses.ToList();
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(Course course)
        {
            List<Department> departments = _DatabaseEntities.Departments.ToList();
            ViewBag.dept = departments;
            try
            {
                if (_DatabaseEntities.Courses.Find(course.ID) == null)
                {
                    _DatabaseEntities.Courses.Add(course);
                    _DatabaseEntities.SaveChanges();
                    String message = "Course added successfully";
                    return RedirectToAction("Index", "Admin", message);
                }
                else
                {
                    ModelState.AddModelError("ID", "This Course is already in the database");
                    return View();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", "An errorr Has Acoured please try again later");
                Console.WriteLine("Error at the Line 82 of AdminController : " + e.Message);
                return View();
            }
        }

        public ActionResult CreateUser()
        {
            ViewBag.dept = _DatabaseEntities.Departments.ToList();

            Dictionary<string, string> role = new Dictionary<string, string>
            {
                { "admin", "System Admin" },
                { "Lecturer", "Lecturer" }
            };
            ViewBag.role = role;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(Lecturer lecturer)
        {

            ViewBag.dept = _DatabaseEntities.Departments.ToList();

            Dictionary<string, string> role = new Dictionary<string, string>
            {
                { "admin", "System Admin" },
                { "Lecturer", "Lecturer" }
            };
            ViewBag.role = role;

            try
            {
                if (_DatabaseEntities.Lecturers.Find(lecturer.ID) == null)
                {
                    if (ModelState.IsValid)
                    {
                        _DatabaseEntities.Lecturers.Add(lecturer);
                        _DatabaseEntities.SaveChanges();
                        String message = "Lecturer added successfully";
                        return RedirectToAction("Index", "Admin", message);
                    }
                    return View();
                }
                else
                {

                    ModelState.AddModelError("Dublicate Value", " lecturer ID is already in the database");
                    return View();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", "An errorr Has Acoured please try again later");
                Console.WriteLine("Error at the Line 82 of AdminController : " + e.Message);
                return View();
            }
        }

        public ActionResult CreateDpt()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDpt(Department department)
        {
            try
            {
                if (_DatabaseEntities.Departments.Find(department.ID) == null)
                {
                    if (ModelState.IsValid)
                    {
                        _DatabaseEntities.Departments.Add(department);
                        _DatabaseEntities.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View();

                }
                else
                {
                    ModelState.AddModelError("Dublicate Value", " department ID is already in the database");
                    return View();
                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", "An errorr Has Acoured please try again later");
                Console.WriteLine("Error at the Line 82 of AdminController : " + e.Message);
                return View();
            }

        }
        public ActionResult CreateFaculty()
        {
            Faculty faculty = new Faculty()
            {
                PassingGradeForPI = 60,
                PassingGradeForAssessmentSurvey = 3
            };
            return View(faculty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreateFaculty(Faculty faculty)
        {
            try
            {
                _DatabaseEntities.Faculties.Add(faculty);
                _DatabaseEntities.SaveChanges();
                String message = "Student added successfully";
                return RedirectToAction("Index", "Admin", message);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(e.Message, "an error has accoured please try again later ");
                Console.WriteLine("Error at line 384 Admin" + e);
                return View();
            }
        }

        public ActionResult EditDpt(int? id)
        {
            ViewBag.faculty = _DatabaseEntities.Faculties.ToList();


            try
            {
                Department d = _DatabaseEntities.Departments.Find(id);
                if(d!=null)
                    ViewBag.Lecturer = _DatabaseEntities.Lecturers.Where(x => x.dptId == id).ToList();
                 return View(d);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new HttpStatusCodeResult(404);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDpt(Department department)
        {

            ViewBag.faculty = _DatabaseEntities.Faculties.ToList();
            Department d = _DatabaseEntities.Departments.Find(department.ID);
            if (d == null)
            {
                _DatabaseEntities.Departments.Add(department);
                _DatabaseEntities.SaveChanges();
                return RedirectToAction("ListDepartments");

            }

            d.Lecturer = department.Lecturer;
            d.Name = department.Name;
            d.FacultyId = department.FacultyId;
            d.HeadOFDepartment = department.HeadOFDepartment;
            _DatabaseEntities.Entry(d).State = EntityState.Modified;
            _DatabaseEntities.SaveChanges();
            return RedirectToAction("ListDepartments");
        }


        public ActionResult EditFaculty(int? id)
        {

            ViewBag.Lecturer = _DatabaseEntities.Lecturers.Where(x => x.Department.FacultyId == id).ToList(); 
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            Faculty faculty = _DatabaseEntities.Faculties.Find(id);
            if (faculty == null)
            {
                return new HttpStatusCodeResult(404);
            }
            else
            {
                return View(faculty);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditFaculty(Faculty faculty)
        {
            ViewBag.Lecturer = _DatabaseEntities.Lecturers.Where(x => x.Department.FacultyId == faculty.ID).ToList();
            if (ModelState.IsValid)
            {
                _DatabaseEntities.Entry(faculty).State = EntityState.Modified;
                _DatabaseEntities.SaveChanges();
                string updateMassege = faculty.Name + "Updated successfully";
                return RedirectToAction("ListFaculty", "Admin", updateMassege);
            }
            return View();
        }

        public ActionResult CreateNewSemster()
        {
            Dictionary<string, string> Semester = new Dictionary<string, string>
            {
                { "1", "First Semester" },
                { "2", "Second Semester" },
                { "3", "Third Semester" }
            };
            ViewBag.Semester = new SelectList(Semester, "Key", "Value");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewSemster(YearAndSemester YAS)
        {
            int y = YAS.Year.Year;
            YAS.Year = new DateTime(y, 1, 1);

            Dictionary<string, string> Semester = new Dictionary<string, string>
            {
                { "1", "First Semester" },
                { "2", "Second Semester" },
                { "3", "Third Semester" }
            };

            ViewBag.Semester = new SelectList(Semester, "Key", "Value");
            if (ModelState.IsValid)
            {
                _DatabaseEntities.YearAndSemesters.Add(YAS);
                _DatabaseEntities.SaveChanges();
                String message = "New Semester added successfully";
                return RedirectToAction("Index", "Admin", message);
            }

            return View();
        }

        [HttpGet]
        public ActionResult ListCourses(string? message, string? Search, int? Department, int page = 1, int pageSize = 10)
        {
            ViewBag.message = message;
            if ((Search == null || Search == "") && Department == null)
            {
                List<Course> Courses = _DatabaseEntities.Courses.ToList();
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                ViewBag.Department = new SelectList(departments, "ID", "Name");
                PagedList<Course> CoursesPaged = new PagedList<Course>(Courses, page, pageSize);
                return View(CoursesPaged);
            }
            else if (Search == null || Search == "")
            {
                List<Course> Courses = _DatabaseEntities.Courses.Where(x => x.dptid == Department).ToList();
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                ViewBag.Department = new SelectList(departments, "ID", "Name");
                PagedList<Course> CoursesPaged = new PagedList<Course>(Courses, page, pageSize);
                ViewBag.Search = Search;
                ViewBag.DepartmentID = Department;
                return View(CoursesPaged);

            }
            else if (Department == null)
            {
                List<Course> Courses = _DatabaseEntities.Courses.Where(x => x.Title.ToLower().Contains(Search.ToLower()) || x.ID.ToLower().Contains(Search.ToLower())).ToList();
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                ViewBag.Department = new SelectList(departments, "ID", "Name");
                PagedList<Course> CoursesPaged = new PagedList<Course>(Courses, page, pageSize);
                ViewBag.Search = Search;
                ViewBag.DepartmentID = Department;

                return View(CoursesPaged);
            }
            else
            {
                List<Course> Courses = _DatabaseEntities.Courses.Where(x => x.Title.ToLower().Contains(Search.ToLower()) || x.ID.ToLower().Contains(Search.ToLower())).ToList();
                Courses = Courses.Where(x => x.dptid.Equals(Department)).ToList();
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                ViewBag.Department = new SelectList(departments, "ID", "Name");
                PagedList<Course> CoursesPaged = new PagedList<Course>(Courses, page, pageSize);
                ViewBag.Search = Search;
                ViewBag.DepartmentID = Department;
                return View(CoursesPaged);
            }
        }


        public ActionResult ListStudents(string? Search, int? Department, string? updateMassege, int page = 1, int pageSize = 10)
        {
            ViewBag.updateMassege = updateMassege;
            if ((Search == null || Search == "") && Department == null)
            {
                List<Student> students = _DatabaseEntities.Students.ToList();
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                PagedList<Student> studentsPaged = new PagedList<Student>(students, page, pageSize);
                ViewBag.Department = new SelectList(departments, "ID", "Name");
                ViewBag.Search = Search;
                ViewBag.DepartmentID = Department;
                return View(studentsPaged);


            }
            else if (Search == null || Search == "")
            {
                List<Student> students = _DatabaseEntities.Students.Where(x => x.DptID == Department).ToList();
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                PagedList<Student> studentsPaged = new PagedList<Student>(students, page, pageSize);
                ViewBag.Department = new SelectList(departments, "ID", "Name");
                ViewBag.Search = Search;
                ViewBag.DepartmentID = Department;
                return View(studentsPaged);
            }
            else if (Department == null)
            {
                List<Student> students = _DatabaseEntities.Students.Where(x => x.ID.ToLower().Contains(Search.ToLower())).ToList();
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                PagedList<Student> studentsPaged = new PagedList<Student>(students, page, pageSize);
                ViewBag.Department = new SelectList(departments, "ID", "Name");
                ViewBag.Search = Search;
                ViewBag.DepartmentID = Department;
                return View(studentsPaged);
            }
            else
            {
                List<Student> students = _DatabaseEntities.Students.Where(x => x.ID.StartsWith(Search)).ToList();
                students = students.Where(x => x.DptID == Department).ToList();
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                PagedList<Student> studentsPaged = new PagedList<Student>(students, page, pageSize);
                ViewBag.Department = new SelectList(departments, "ID", "Name");
                ViewBag.Search = Search;
                ViewBag.DepartmentID = Department;
                return View(studentsPaged);
            }
        }


        public ActionResult ListDepartments(string? Search, string? updateMassege, int page = 1, int pageSize = 10)
        {
            ViewBag.updateMassege = updateMassege;

            if (Search == null)
            {
                List<Department> departments = _DatabaseEntities.Departments.ToList();
                PagedList<Department> departmentsPaged = new PagedList<Department>(departments, page, pageSize);
                return View(departmentsPaged);
            }
            else
            {

                int.TryParse(Search, out int s);

                List<Department> departments = _DatabaseEntities.Departments.Where(x => x.Name.Equals(Search) || x.ID.Equals(s)).ToList();
                PagedList<Department> departmentsPaged = new PagedList<Department>(departments, page, pageSize);
                return View(departmentsPaged);
            }
        }


        public ActionResult UpdateStudent()
        {
            ViewBag.Departments = _DatabaseEntities.Departments.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStudent(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _DatabaseEntities.Entry(student).State = EntityState.Modified;
                    _DatabaseEntities.SaveChanges();

                    return RedirectToAction("ListStudents");
                }
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(e.Message, "an error has accoured please try again later ");
                Console.WriteLine("Error at line 365  Admin" + e);
                return View();
            }


        }

        public ActionResult CreateStudent()
        {
            ViewBag.Departments = _DatabaseEntities.Departments.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _DatabaseEntities.Students.Add(student);
                    _DatabaseEntities.SaveChanges();

                }
                String message = "Student added successfully";
                return RedirectToAction("ListSudents", "Admin", message);

            }
            catch (Exception e)
            {
                ModelState.AddModelError(e.Message, "an error has accoured please try again later ");
                Console.WriteLine("Error at line 384 Admin" + e);


                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudentList(FormCollection formCollection)
        {
            int added = 0;
            int edited = 0;
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["Select Excel file"];
                if ((file != null) && (file.ContentLength != 0) && !string.IsNullOrEmpty(file.FileName))
                {

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        for (int rowIterator = 2; rowIterator < noOfRow; rowIterator++)
                        {
                            if (workSheet.Cells[rowIterator, 1].Value != null && workSheet.Cells[rowIterator, 2].Value != null && workSheet.Cells[rowIterator, 3].Value != null)
                                try
                                {
                                    var Student = new Student
                                    {
                                        ID = workSheet.Cells[rowIterator, 1].Value.ToString(),
                                        Name = workSheet.Cells[rowIterator, 2].Value.ToString(),
                                        DptID = int.Parse(workSheet.Cells[rowIterator, 3].Value.ToString()),
                                    };
                                    if (_DatabaseEntities.Students.Find(Student.ID) == null)
                                    {
                                        _DatabaseEntities.Students.Add(Student);
                                        _DatabaseEntities.SaveChanges();
                                        added++;
                                    }
                                    else
                                    {
                                        _DatabaseEntities.Entry(Student).State = EntityState.Modified;
                                        _DatabaseEntities.SaveChanges();
                                        edited++;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error at 235 CourseCoorddinatorController" + e);
                                }
                            rowIterator++;
                        }
                    }
                }

            }
            string updateMassege = (added + " new student added /n  " + edited + " student info changed");
            return RedirectToAction("ListStudents", "Admin", new { updateMassege });
        }

        //todo add list  Faculty,

        public ActionResult ListFaculty(string? Search, string? updateMassege, int page = 1, int pageSize = 10)
        {
            ViewBag.updateMassege = updateMassege;


            if (Search == null)
            {
                List<Faculty> Faculties = _DatabaseEntities.Faculties.ToList();
                PagedList<Faculty> FacultiesPaged = new PagedList<Faculty>(Faculties, page, pageSize);
                return View(FacultiesPaged);
            }
            else
            {

                int.TryParse(Search, out int s);

                List<Faculty> Faculties = _DatabaseEntities.Faculties.Where(x => x.Name.Equals(Search) || x.ID.Equals(s)).ToList();
                PagedList<Faculty> FacultiesPaged = new PagedList<Faculty>(Faculties, page, pageSize);
                return View(FacultiesPaged);
            }
        }


        public ActionResult  ListUsers(string? Search, int? DeptID, string? updateMassege, int page = 1, int pageSize = 10)
        {

            List<Department> departments = _DatabaseEntities.Departments.ToList();
            ViewBag.Department = new SelectList(departments, "ID", "Name");
            if (Search == null && DeptID == null)
            {
                List<Lecturer> Lecturers = _DatabaseEntities.Lecturers.ToList();
                PagedList<Lecturer> LecturersPaged = new PagedList<Lecturer>(Lecturers, page, pageSize);
                return View(LecturersPaged);
            }
            else if (DeptID == null)
            {

                int.TryParse(Search, out int s);

                List<Lecturer> Lecturers = _DatabaseEntities.Lecturers.Where(x => x.Name.Equals(Search) || x.ID.Equals(s)).ToList();
                PagedList<Lecturer> LecturersPaged = new PagedList<Lecturer>(Lecturers, page, pageSize);
                return View(LecturersPaged);    
            }
            else if (Search == null)
            {
                List<Lecturer> Lecturers = _DatabaseEntities.Lecturers.Where(x=>x.dptId.Equals(DeptID)).ToList();
                PagedList<Lecturer> LecturersPaged = new PagedList<Lecturer>(Lecturers, page, pageSize);
                return View(LecturersPaged);

            }
            else
            {
                int.TryParse(Search, out int s);
                List<Lecturer> Lecturers = _DatabaseEntities.Lecturers.Where(x =>( x.Name.Equals(Search) || x.ID.Equals(s)) && (x.dptId.Equals(DeptID))).ToList();
                PagedList<Lecturer> LecturersPaged = new PagedList<Lecturer>(Lecturers, page, pageSize);
                return View(LecturersPaged);
            }
        }

    }
}

