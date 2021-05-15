using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CoursesModelView
{
    public class CouresReportModelView : CouresModelView
    {
        LSS_databaseEntities db = new LSS_databaseEntities();
        private int _NoOFSection { set; get; }
        public CouresReportModelView(CourseCoordinator courseCoordinator) {
            this.CourseCoordinator = courseCoordinator;

        }

        public int NoOFSection {

            get {
                if (_NoOFSection == 0) {
                    _NoOFSection = db.OtherLecturers.Where(x => x.CourseID.Equals(this.Course.ID)).Count()+1;
                }

                return _NoOFSection;
            }
        }





    }
}