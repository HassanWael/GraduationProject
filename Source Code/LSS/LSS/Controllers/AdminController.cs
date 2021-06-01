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
    //[Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
      readonly  LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        // GET: Admin
        //ToDO :Create Index View for Admin.
      readonly  YearAndSemester YAS = SemesterSingelton.getCurrentYearAndSemester();
        public ActionResult Index(string? message)
        {
            ViewBag.message = message;
            return View();
        }

        public ActionResult AddCourseToSemester(string CourseID)
        {

            CourseCoordinator cc;
            if (_DatabaseEntities.CourseCoordinators.Find(CourseID, YAS.Year, YAS.Semester) == null)
            {
                cc = new CourseCoordinator
                {
                    CourseID = CourseID,
                    Year = YAS.Year,
                    Semseter = YAS.Semester
                };
            }
            else
            {
                cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, YAS.Year, YAS.Semester);
            }
            Course course = _DatabaseEntities.Courses.Find(CourseID);
            int FacultyID = course.Department.FacultyId;
            List<Lecturer> lec = _DatabaseEntities.Lecturers.Where(x => x.Department.FacultyId.Equals(FacultyID)).ToList();

            ViewBag.Lecturers = lec;
            return View(cc);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourseToSemester(CourseCoordinator cc)
        {
            try
            {
                _DatabaseEntities.CourseCoordinators.Add(cc);
                _DatabaseEntities.SaveChanges();
            }
            catch
            {
                try
                {
                    if (_DatabaseEntities.CourseCoordinators.Find(cc.CourseID, cc.Year, cc.Semseter) == null)
                    {
                        _DatabaseEntities.CourseCoordinators.Add(cc);
                        _DatabaseEntities.SaveChanges();
                    }
                    else
                    {
                        _DatabaseEntities.Entry(cc).State = EntityState.Modified;
                        _DatabaseEntities.SaveChanges();

                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("An errorr Has Acoured please try again later", e);
                    Console.WriteLine("Error at the Line 48 of AdminController : " + e.Message);
                    return View();
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreatCourse()
        {
            List<Department> departments = _DatabaseEntities.Departments.ToList();
            ViewBag.dept = departments;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatCourse(Course course)
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

        public ActionResult CreatUser()
        {
            List<Department> departments = _DatabaseEntities.Departments.ToList();
            ViewBag.dept = departments;
            Dictionary<string, string> role = new Dictionary<string, string>();
            role.Add("admin", "System Admin");
            role.Add("Lecturer", "Lecturer");
            ViewBag.role = role;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatUser(Lecturer lecturer)
        {
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

        public ActionResult CreatDpt()
        {

            return View();
        }
        [HttpPost]

        public ActionResult CreatDpt(Department department)
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
        public ActionResult CreatFaculty()
        {
            Faculty faculty = new Faculty()
            {
                PassingGradeForPI = 60,
                PassingGradeForAssessmentSurvey = 3
            };
            return View(faculty);
        }

        [HttpPost]
        public ActionResult CreatFaculty(Faculty faculty)
        {
            try
            {
                _DatabaseEntities.Faculties.Add(faculty);
                _DatabaseEntities.SaveChanges();
                String message = "Student added successfully";
                return RedirectToAction("Index", "Admin", message);
            }
            catch (Exception e )
            {
                ModelState.AddModelError(e.Message, "an error has accoured please try again later ");
                Console.WriteLine("Error at line 384 Admin" + e);
                return View();
            }
        }

        public ActionResult EditDpt(String id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(404);
                }
                Department d = _DatabaseEntities.Departments.Find(id);
                if (d == null)
                {
                    return new HttpStatusCodeResult(404);
                }
                else
                {
                    return View(d);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new HttpStatusCodeResult(404);
            }
        }
        [HttpPost]
        public ActionResult EditDpt(Department department)
        {
            _DatabaseEntities.Entry(department).State = EntityState.Modified;
            _DatabaseEntities.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult EditFaculty(String id)
        {
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
        public ActionResult EditFaculty(Faculty faculty)
        {
            _DatabaseEntities.Entry(faculty).State = EntityState.Modified;
            _DatabaseEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreateNewSemster()
        {
            Dictionary<string, string> semester = new Dictionary<string, string>();
            semester.Add("1", "First semester");
            semester.Add("2", "Second semester");
            semester.Add("3", "Third semester");

            ViewBag.semester = new SelectList(semester, "Key", "Value");
            return View();
        }
        [HttpGet]
        public ActionResult ListCourses(string? Search, int? Department, int page = 1, int pageSize = 10)
        {
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
        public ActionResult UpdateStudent()
        {
            ViewBag.Departments = _DatabaseEntities.Departments.ToList();
            return View();
        }
        [HttpPost]
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

        public ActionResult CreatStudent()
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
                return RedirectToAction("Index","Admin",message);

            }
            catch (Exception e)
            {
                ModelState.AddModelError(e.Message, "an error has accoured please try again later ");
                Console.WriteLine("Error at line 384 Admin" + e);


                return View();
            }
        }

        [HttpPost]
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
            return RedirectToAction("ListStudents", "Admin", new { updateMassege = updateMassege });
        }
    }
}

