using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Schedule> Schedules { get; set; }
    }
}
