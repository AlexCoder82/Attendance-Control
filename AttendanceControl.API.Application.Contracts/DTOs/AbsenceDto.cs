

using AttendanceControl.API.Business.Enums;

namespace AttendanceControl.API.Application.DTOs
{
    public class AbsenceDto
    {
        public int StudentId { get; set; }
        public int SchoolClassId { get; set; }
        public AbsenceType Type {get;set;}   
    }
}
