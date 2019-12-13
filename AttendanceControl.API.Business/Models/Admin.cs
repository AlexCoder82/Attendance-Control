using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string AdminName { get; set; }
        public string Password { get; set; }
    }
}
