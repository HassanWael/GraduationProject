using LSS.Models;
using LSS.Models.arc;
using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LSS.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Student
        // TODO: Add the Student Stuff here the CourseCoordintor is 2 crowded  
       private readonly LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        private readonly YearAndSemester yas = SemesterSingelton.getCurrentYearAndSemester();

        [HttpPost]
        public ActionResult RemoveStudentFromCourse(string? StudetnID, string? CourseID, DateTime? Year, string Semseter)
        {
            try
            {
                EnroledStudent s = _DatabaseEntities.EnroledStudents.Find(StudetnID, CourseID, Year, Semseter);
                if (s != null)
                {
                    _DatabaseEntities.EnroledStudents.Remove(s);
                    _DatabaseEntities.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Index", "LogedIn");
                }
            }
            catch
            {
                return RedirectToAction("CourseStudentList", new { CourseID, Year, Semseter });
            }

            return RedirectToAction("CourseStudentList", new { CourseID, Year, Semseter });
        }

        public ActionResult AddStudentListToCourse(FormCollection formCollection, string CourseID, DateTime Year, string Semseter)
        {
            int added = 0;
            int dicarded = 0;

            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["Select Excel file"];
                if ((file != null) && (file.ContentLength != 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semseter);
                        if (cc != null)
                        {
                            for (int rowIterator = 2; rowIterator < noOfRow; rowIterator++)
                            {
                                if (workSheet.Cells[rowIterator, 1].Value != null && workSheet.Cells[rowIterator, 2].Value != null && workSheet.Cells[rowIterator, 3].Value != null)
                                    try
                                    {
                                        var Student = _DatabaseEntities.Students.Find(workSheet.Cells[rowIterator, 1].Value.ToString());

                                        if (Student != null)
                                        {
                                            EnroledStudent e = new EnroledStudent()
                                            {
                                                StudentID = Student.ID,
                                                CourseID = cc.CourseID,
                                                Year = cc.Year,
                                                Semseter = cc.Semseter
                                            };
                                            _DatabaseEntities.EnroledStudents.Add(e);
                                            _DatabaseEntities.SaveChanges();

                                            added++;
                                        }
                                        else
                                        {
                                            dicarded++;

                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Error at 340 CourseCoorddinatorController" + e);
                                    }
                                rowIterator++;
                            }
                        }
                        else
                        {
                            return RedirectToAction("Index", "LogedIn");
                        }
                    }
                }
            }
            string updateMassege = (added + " student has been added to the Course.");
            return RedirectToAction("CourseStudentList", new { CourseID, updateMassege, Year, Semseter });
        }

        public ActionResult UploadGrades(FormCollection formCollection, string CourseID, DateTime Year, string Semseter)
        {
            int failedToAddGraddeTo = 0; 
            EnroledStudent enroledStudent; 
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["Select Excel file"];

                if ((file != null) && (file.ContentLength != 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                }
                using (var package = new ExcelPackage(file.InputStream))
                {
                    var currentSheet = package.Workbook.Worksheets;
                    var workSheet = currentSheet.First();
                    var noOfCol = workSheet.Dimension.End.Column;
                    var noOfRow = workSheet.Dimension.End.Row;
                    CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find(CourseID, Year, Semseter);
                    if (cc != null)
                    {
                        for (int rowIterator = 2; rowIterator < noOfRow; rowIterator++)
                        {
                            if (workSheet.Cells[rowIterator, 1].Value != null && workSheet.Cells[rowIterator, 2].Value != null)
                                try
                                {
                                    enroledStudent = _DatabaseEntities.EnroledStudents.Where(x => x.StudentID.Equals(workSheet.Cells[rowIterator, 1].Value.ToString()) 
                                                        && x.CourseID.Equals(CourseID) &&x.Year.Equals(Year)&& x.Semseter.Equals(Semseter)).FirstOrDefault();

                                    if (enroledStudent != null)
                                    {
                                        enroledStudent.FinalGrade = int.Parse(workSheet.Cells[rowIterator, 2].Value.ToString());
                                        _DatabaseEntities.Entry(enroledStudent).State = System.Data.Entity.EntityState.Modified;
                                    }
                                    else
                                    {
                                        failedToAddGraddeTo++;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error at 340 CourseCoorddinatorController" + e);
                                }
                            rowIterator++;
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "LogedIn");
                    }
                }
            }
                return RedirectToAction("CourseStudentList","Student",new { CourseID= CourseID , Year = Year , Semseter = Semseter });
        }

        public ActionResult CourseStudentList(string? CourseID, string? updateMassege, DateTime? Year, string? Semester, int? Department, string? Search, int page = 1, int pageSize = 10)
        {
            if (Year == null && Semester == null)
            {
                Year = yas.Year;
                Semester = yas.Semester;
            }
            //if (CourseID == null)
            //{
            //    return RedirectToAction("Index", "LogedIn");
            //}
            CourseCoordinator cc = _DatabaseEntities.CourseCoordinators.Find("A0334501", Year, Semester);

            if (cc == null)
            {
                return RedirectToAction("Index", "LogedIn");

            }
            PagedList<EnroledStudent> studentsPaged;
            List<EnroledStudent> CourseStudents;
            ViewBag.Search = Search;
            ViewBag.CourseID = CourseID;
            ViewBag.Year = Year;
            ViewBag.Semester = Semester;


            if ((Search == null || Search.Equals("")))
            {
                CourseStudents = cc.EnroledStudents.Where(x => x.Student.DptID.Equals(Department)).ToList();
                studentsPaged = new PagedList<EnroledStudent>(CourseStudents, page, pageSize);

            }
            else
            {
                CourseStudents = cc.EnroledStudents.Where(x => x.Student.ID.Equals(Search) || x.Student.Name.Contains(Search)).ToList();
                studentsPaged = new PagedList<EnroledStudent>(CourseStudents, page, pageSize);
            }
            return View(studentsPaged);
        
        }
        
      
        
        
    
    }
}