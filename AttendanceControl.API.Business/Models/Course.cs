﻿using System.Collections.Generic;

namespace AttendanceControl.API.Business.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public IEnumerable<SchoolClass> SchoolClasses { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
    }
}
