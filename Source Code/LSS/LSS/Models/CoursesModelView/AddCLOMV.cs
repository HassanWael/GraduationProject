using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.CoursesModelView
{
    public class AddCLOMV
    {
        LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        public AddCLOMV()
        {
        }   
        public AddCLOMV(CLO CLO)
        {
            this.CLO = CLO; 
        }
        private int? deptID{ get; set; }
        public int? DpetID
        {
            get
            {
                if(deptID==null)
                    deptID= _DatabaseEntities.Courses.Where(c => c.ID.Equals(CLO.courseId)).Select(x => x.dptid).FirstOrDefault();
                return deptID;
            }
        }

        public AddCLOMV(String courseID)
        {
            CLO = new CLO(courseID);
        }

        public CLO CLO { get; set; }
        private List<PI> _DepratmentPI { get; set; }
        public List<PI> DepratmentPI 
        {
            get
            {
                if (_DepratmentPI == null)
                {
                    _DepratmentPI = _DatabaseEntities.PIs.Where(x => x.DeptID== DpetID).ToList();
                }
                return _DepratmentPI;
            }
        }

        private List<String> _SelectedPIs { get; set; }
        public List<String> SelectedPIs
        {
            get
            {
                if (_SelectedPIs == null)
                {
                    _SelectedPIs = CLO.PIs.Select(m => m.ID).ToList();
                }

                return _SelectedPIs;
            }
            set
            {
                _SelectedPIs = value;
            }
        }
    }
}