using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSS.Models.DepartmentViewModel
{
    public class MappedPEO_SLO
    {
        private readonly LSS_databaseEntities _databaseEntities = new LSS_databaseEntities();
        public Department Department { get; set; }
        private List<PEO> peos
        {
            get; set;
        }

        public List<PEO> PEOs
        {
            get
            {
                if (peos == null)
                {
                    peos = Department.PEOs.ToList();
                }
                return peos;
            }

            set
            {
                peos = value;
            }
        }

        private List<SLO> sloes { get; set; }
        public List<SLO> SLOes {
            get
            {
                if (sloes == null)
                {
                    sloes = Department.SLOes.ToList();
                }
                return sloes;
            }
            set
            {
                sloes = value;
            }
        }

        private HashSet<SLO_PEO> slo_peo { get; set; }
        public HashSet<SLO_PEO> SLO_PEO
        {
            get
            {
                if (slo_peo == null)
                {
                    slo_peo = new HashSet<SLO_PEO>();
                    foreach (SLO slo in SLOes)
                    {
                        slo.SLO_PEO.ToList().ForEach(x => slo_peo.Add(x));
                    }
                }

                return slo_peo;
            }
            set
            {
                slo_peo = value;
            }
        }
        private List<PEO> unmappedPEO { get; set; }
        public List<PEO> UnmappedPEO {
            get
            {
                if (unmappedPEO == null)
                {
                    unmappedPEO = new List<PEO>();

                    if (SLO_PEO != null)
                    {
                        foreach (PEO peo in PEOs)
                        {
                            if (!SLO_PEO.Select(x => x.PEO).Contains(peo))
                            {
                                unmappedPEO.Add(peo);
                            }
                        }
                    }
                    else
                    {
                        unmappedPEO = PEOs;
                    }

                }
                return unmappedPEO;
            }
        }
        private List<SLO> unmappedSLO { get; set; }
        public List<SLO> UnmappedSLO
        {
            get
            {
                if (unmappedSLO == null)
                {
                    unmappedSLO = new List<SLO>();

                    foreach (SLO slo in SLOes)
                    {
                        if (SLO_PEO != null)
                        {
                            if (SLO_PEO.Where(x=>x.SLOID.Equals(slo.SLOID)).FirstOrDefault()==null)
                            {
                                unmappedSLO.Add(slo);
                            }
                        }
                        else
                        {
                            unmappedSLO = SLOes;
                        }
                    }
                }
                return unmappedSLO;
            }
        } 

       public MappedPEO_SLO(Department department)
        {
            Department = department;
           
        }
    }
}