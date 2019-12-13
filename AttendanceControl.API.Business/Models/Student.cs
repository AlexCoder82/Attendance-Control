using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Models
{
    public class Student:Person
    {
        public int TotalAbsences { get; set; }
        public int TotalDelays { get; set; }
        public  IEnumerable<Absence> Absences { get; set; }
    }
}
