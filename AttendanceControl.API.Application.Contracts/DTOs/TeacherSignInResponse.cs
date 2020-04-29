using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Application.Contracts.DTOs
{
    public class TeacherSignInResponse
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
        public  string Role { get; set; }
        public List<SchoolClass> SchoolClasses { get; set; }
    }
}
