using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Models
{
    public class Student:Person
    {
        public Course Course { get; set; }
        public int TotalAbsences { get; set; }
        public int TotalDelays { get; set; }
        public  IEnumerable<Absence> Absences { get; set; }
    }
}
