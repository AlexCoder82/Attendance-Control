using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Models
{
    public class Cycle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Course[] Courses { get; set; }
    }
}
