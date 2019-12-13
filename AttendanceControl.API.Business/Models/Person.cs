using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public   IEnumerable<Subject> Subjects { get; set; }
    }
}
