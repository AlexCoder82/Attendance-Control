using System.Collections.Generic;

namespace AttendanceControl.API.Business.Models
{
    public class Cycle
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public  IEnumerable<Subject> Subjects { get; set; }
    }
}
