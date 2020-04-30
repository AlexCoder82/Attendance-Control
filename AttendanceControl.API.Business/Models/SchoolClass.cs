using System;
using System.Collections.Generic;

namespace AttendanceControl.API.Business.Models
{
    public class SchoolClass
    {     
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }   
        public  Subject Subject { get; set; }
        public  Schedule Schedule { get; set; }
        public Course Course { get; set; }
        public List<Student> Students { get; set; }       
    }
}
