using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CoursesModelView
{
    /// <summary>
    ///       this model is to composit multiple previously created models togther to pass to the view
    /// </summary>
    
    //ToDo complete all the set and get 
    public class CIFModelView
    {
        LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        public Course Course { get; set; }
        public Department Department { get { return Course.Department; } }
        public Faculty Faculty { get { return Department.Faculty; } }
        private YearAndSemester _yearAndSemester { get; set; }
        public YearAndSemester YearAndSemester { 
            get { 
                if(_yearAndSemester==null)
                    _yearAndSemester= _DatabaseEntities.YearAndSemesters.FirstOrDefault();
                return _yearAndSemester;
            } 
            set
            {
                _yearAndSemester= value; 
            }
        }
        private CourseCoordinator _CourseCoordinator { get; set; }
        public CourseCoordinator CourseCoordinator { 
            get 
            {
                if (_CourseCoordinator == null)
                {
                    _CourseCoordinator = _DatabaseEntities.CourseCoordinators
                        .Where(x => x.CourseID.Equals(Course.ID)
                            && x.Year.Equals(YearAndSemester.Year)
                            && x.Semseter.Equals(YearAndSemester.Semester)).FirstOrDefault();
                }
                return _CourseCoordinator;
            }
        }
        public IEnumerable<SLO> SLOes { get; set; }
        private List<String>_selectedSLOes {get;set;}
        public List<String> SelectedSLOes
        {
            get
            {
                if (_selectedSLOes == null)
                {
                    _selectedSLOes = Course.SLOes.Select(m => m.SLOID).ToList();
                }
                return _selectedSLOes;
            }
            set
            {
                _selectedSLOes = value;
            }

        }
        public IEnumerable<PI> PIs { get; set; }
        private List<String> _selectedPIs { get; set;}
        public List<String> SelectedPIs
        {
            get
            {
                if (_selectedPIs == null)
                {
                    _selectedPIs = Course.PIs.Select(m => m.ID).ToList();
                }
                return _selectedPIs;
            }
            set
            {
                _selectedPIs = value;
            }

        }
        public List<CLO> CLOs { get; set; }
        public IEnumerable<AssessmentPlanforTheStudentLearningOutcomeTechnique> APFSLOs { get; set; }
        private List<int> _selectedAPFSLOs { get; set; }
        public List<int> SelectedAPFSLOs
        {
            get
            {
                if (_selectedAPFSLOs == null)
                {
                    _selectedAPFSLOs = CourseCoordinator.AssessmentPlanforTheStudentLearningOutcomeTechniques.Select(m => m.ID).ToList();
                }
                return _selectedAPFSLOs;
            }
            set
            {
                _selectedAPFSLOs = value;
            }

        }
        public CourseInformationForm CourseInformationForm { get; set; }
      
    
 
    }
}