using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Application.Contracts.DTOs
{
    public class SchoolClassStudent
    {
        public Student Student { get; set; }
        public int SchoolClassID { get; set; }
        public Absence Absence { get; set; }
      
    }
}
