
using AttendanceControl.API.Business.Enums;

namespace AttendanceControl.API.Business.Models
{
    public class Absence
    {
        public int Id { get; set; }
        public AbsenceType Type { get; set; }
        public  Schedule Schedule { get; set; }
        public  Student Student { get; set; }
    }
}
