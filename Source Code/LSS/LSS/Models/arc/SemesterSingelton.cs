using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.arc
{
    public class SemesterSingelton
    {

        private LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        private SemesterSingelton(){ yearAndSemester = _DatabaseEntities.YearAndSemesters.FirstOrDefault(); }
        private static SemesterSingelton semesterSingelton;
        private static YearAndSemester yearAndSemester;
        public static YearAndSemester getCurrentYearAndSemester()
        {
            if (semesterSingelton == null)
            {
                semesterSingelton = new SemesterSingelton(); 
            }
            return yearAndSemester;
        }
        
        public static void updateCurrentSemester()
        {
            semesterSingelton = new SemesterSingelton();
        }
    }
}