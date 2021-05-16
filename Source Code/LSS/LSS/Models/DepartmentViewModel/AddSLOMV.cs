using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.DepartmentViewModel
{
    public class AddSLOMV
    {
      private readonly LSS_databaseEntities _DatabaseEntities = new LSS_databaseEntities();
        public AddSLOMV(int deptID)
        {
          
            SLO = new SLO(deptID);
        }
        public AddSLOMV(SLO SLO)
        {
            this.SLO = SLO;
        }
        public AddSLOMV()
        {
            
        }
        public SLO SLO { get; set; }

        private List<PEO> peos { get; set; }

        public List<PEO> PEOs { get
            {
                if (peos == null)
                {
                    peos = _DatabaseEntities.PEOs.Where(x => x.DeptID.Equals(SLO.DeptID)).ToList();
                }
                return peos;
            }
        }


        private List<String> _SelectedPEOsID { get; set; }
        public List<String> SelectedPEOsID{
            get
            {
                if (_SelectedPEOsID == null)
                {
                    _SelectedPEOsID = SLO.SLO_PEO.Select(x => x.PEOID).ToList();
                }
                return _SelectedPEOsID;
            }
            set
            {
                _SelectedPEOsID = value;
            }
        }
    }
     
}