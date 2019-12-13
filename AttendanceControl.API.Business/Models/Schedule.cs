
using AttendanceControl.API.Business.Enums;
using System;
using System.Collections.Generic;

namespace AttendanceControl.API.Business.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public Day Day { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Subject Subject { get; set; }
        public IEnumerable<Absence> Absences { get; set; }
    }
}
