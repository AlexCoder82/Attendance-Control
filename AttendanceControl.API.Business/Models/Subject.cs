using System.Collections.Generic;

namespace AttendanceControl.API.Business.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public IEnumerable<Cycle> Cycles { get; set; }
        public IEnumerable<Schedule> Schedules { get; set; }
    }
}
