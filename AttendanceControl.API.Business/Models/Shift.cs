using System.Collections.Generic;

namespace AttendanceControl.API.Business.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
    }
}
