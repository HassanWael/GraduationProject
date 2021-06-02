using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CoursesModelView
{
    public class AddBookMV
    { 

        public AddBookMV() { }
       public  AddBookMV(CourseTextBook? book,string? CourseID ,YearAndSemester? YAS)
        {
            if (book == null)
            {
                this.book = new CourseTextBook();
                this.book.Course = CourseID;
            }
            else
            {
                this.book = book;
            }
            this.YAS = YAS;
        }

        public CourseTextBook book { get; set; }
        public YearAndSemester YAS { get; set; }
    }
}