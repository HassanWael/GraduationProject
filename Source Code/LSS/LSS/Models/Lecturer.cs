﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSS.Models
{
    public class Lecturer :FacultyMember
    {
        public Deparment Deparment { get; set; }
    }
}
