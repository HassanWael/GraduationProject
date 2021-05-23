using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CoursesModelView
{
    public class CouresReportModelView : CouresModelView
    {
        LSS_databaseEntities db = new LSS_databaseEntities();

        public CouresReportModelView(CourseCoordinator courseCoordinator)
        {
            this.CourseCoordinator = courseCoordinator;
        }


        private int _NoOFSection { set; get; }
        private List<string> _SLO { set; get; }
       
      
        public int NoOFSection {

            get {
                if (_NoOFSection == 0) {
                    _NoOFSection = db.OtherLecturers.Where(x => x.CourseID.Equals(this.Course.ID)).Count()+1;
                }

                return _NoOFSection;
            }
        }

        public List<string> SLO
        {

            get {

                _SLO.Add("New York");
                _SLO.Add("London");
                _SLO.Add("Mumbai");
                _SLO.Add("Chicago");
                return _SLO;
            }
       
        }





    }
}