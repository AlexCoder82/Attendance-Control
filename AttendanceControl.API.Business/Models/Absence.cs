
using AttendanceControl.API.Business.Enums;
using System;

namespace AttendanceControl.API.Business.Models
{
    public class Absence
    {
        public int Id { get; set; }
        public AbsenceType Type { get; set; }
        public DateTime Date { get; set; }
        public  Schedule Schedule { get; set; }
        public Subject Subject { get; set; }
        public bool IsExcused { get; set; }
    }
}
