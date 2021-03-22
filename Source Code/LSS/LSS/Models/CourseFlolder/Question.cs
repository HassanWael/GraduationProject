using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSS.Models.CourseFlolder.Accriditation;

namespace LSS.Models.CourseFlolder
{
    public class Question
    {
        public String QuestionStr{ get; set; }

        public String Answer { get; set; }
        public List<SLO> SlOs { get; set; }
    }
}
