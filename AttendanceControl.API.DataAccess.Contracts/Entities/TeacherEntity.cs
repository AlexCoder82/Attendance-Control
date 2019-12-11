using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    public class TeacherEntity: PersonEntity
    {
        public string UserName {get;set;}
        public string Password { get; set; }   
    }
}
