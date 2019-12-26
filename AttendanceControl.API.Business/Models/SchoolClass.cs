using AttendanceControl.API.Business.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Models
{
    public class SchoolClass
    {
        
        public int Id { get; set; }
        public Day Day { get; set; }   
        public  Subject Subject { get; set; }
        public  Schedule Schedule { get; set; }
        public Course Course { get; set; }
        
    }
}
