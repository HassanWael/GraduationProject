using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CoursesModelView
{
    public class AssessingPI
    {
       private static readonly  LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();

        public static int getBelowStander(int QID , int deptID)
        {
            int count = _DatabaseEntities.CourseExamEvals.Where(x => x.QID.Equals(QID) && x.EnroledStudent.Student.DptID.Equals(deptID)  && ((x.Mark * 100) / x.CourseExamQuestion.Weight <= 39)).Count();
            return count; 
        }
        public static int getApproachesStandard(int QID, int deptID)
        {
            int count = _DatabaseEntities.CourseExamEvals.Where(x => x.QID.Equals(QID) && x.EnroledStudent.Student.DptID.Equals(deptID) && ((x.Mark*100)/x.CourseExamQuestion.Weight > 39) && ((x.Mark * 100) / x.CourseExamQuestion.Weight <= 59)).Count();
            return count;
        }
        public static int getMeetsStandard(int QID, int deptID)
        {
            int count = _DatabaseEntities.CourseExamEvals.Where(x => x.QID.Equals(QID) && x.EnroledStudent.Student.DptID.Equals(deptID) && ((x.Mark * 100) / x.CourseExamQuestion.Weight > 59) && ((x.Mark * 100) / x.CourseExamQuestion.Weight <= 79)).Count();
            return count;
        }
        public static int getExceedsStandard(int QID, int deptID)
        {
            int count = _DatabaseEntities.CourseExamEvals.Where(x => x.QID.Equals(QID) && x.EnroledStudent.Student.DptID.Equals(deptID) && ((x.Mark * 100) / x.CourseExamQuestion.Weight > 79) && ((x.Mark * 100) / x.CourseExamQuestion.Weight <= 100)).Count();
            return count;
        }

        public static int NoOfAssessedStudents(int QID, int deptID)
        {
          return _DatabaseEntities.CourseExamEvals.Where(x => x.QID.Equals(QID) && x.EnroledStudent.Student.DptID.Equals(deptID)).Count();
        }
        public static int NoOfDiscardedStudents(int QID,int deptID)
        {
            CourseExamQuestion courseExamQuestion = _DatabaseEntities.CourseExamQuestions.Find(QID);
            int allStudentsCount= courseExamQuestion.CourseExam.CourseCoordinator.EnroledStudents.Where(x=>x.Student.DptID.Equals(deptID)).Count();
            return allStudentsCount- NoOfAssessedStudents(QID,deptID);

        }

        public static double getPoorPercentage(int QID, int deptID)
        {
            int count = _DatabaseEntities.CourseExamEvals.Where(x => x.QID.Equals(QID)).Count();
            double  poorPercentage =1.0*(getBelowStander(QID, deptID) + getApproachesStandard(QID, deptID))/ NoOfAssessedStudents(QID, deptID);
            return poorPercentage;
        }

        public static double getGoodPercentage(int QID, int deptID)
        {
            int count = _DatabaseEntities.CourseExamEvals.Where(x => x.QID.Equals(QID)).Count();
            double goodPercentage = 1.0 * (getMeetsStandard(QID, deptID) + getExceedsStandard(QID, deptID)) / NoOfAssessedStudents(QID, deptID);

            return goodPercentage;
        }

        public static double getPoorPercentageAVG(List<int> QID  , int deptID)
        {
            double avg = 0; 
            
            foreach(int i in QID)
            {
               avg+= getPoorPercentage(i, deptID);
            }


            return avg/QID.Count();
        }
        public static double getGoodPercentageAVG(List<int> QID, int deptID)
        {
            double avg = 0;

            foreach (int i in QID)
            {
                avg += getGoodPercentage(i, deptID);
            }
            return avg / QID.Count();
        }



    }
}