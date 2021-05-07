using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.DepartmentViewModel
{
    public class MappedPEO_SLO
    {
        private readonly LSS_databaseEntities _databaseEntities = new LSS_databaseEntities();

        public List<PEO> PEOs { get; set; }
        public List<SLO> SLOes { get; set; }
        public Boolean[,] Mapping { get; set; }
        int[] sum { get; }
        public PEO [] unmappedPEO { get; set; }
       public MappedPEO_SLO(int dptID)
        {
            PEOs = _databaseEntities.PEOs.Where(peo => peo.DeptID.Equals(dptID)).ToList();
            SLOes = _databaseEntities.SLOes.Where(slo => slo.DeptID.Equals(dptID)).ToList();
            Mapping = new Boolean[PEOs.Count(), SLOes.Count()];
            sum = new int[PEOs.Count()];
            for (int i = 0; i < PEOs.Count(); i++)
            {

                for (int j = 0; j < SLOes.Count(); j++)
                {
                    var map = _databaseEntities.SLO_PEO.Where(x => x.DeptID.Equals(dptID) && x.PEO.Equals(PEOs[i].ID) && x.SLO.Equals(SLOes[j].SLOID)).FirstOrDefault();
                    if (map != null)
                    {
                        sum[i]++;
                        Mapping[i, j] = true;
                        map = null;
                    }
                    else
                    {
                        Mapping[i, j] = false;
                    }
                }
            }

            for (int i = 0; i < PEOs.Count(); i++)
            {
                if (sum[i] <= 0)
                {
                    unmappedPEO[i] = PEOs[i];
                }
            }

        }
    }
}