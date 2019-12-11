using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    public class AdminEntity
    {
        public int Id { get; set; }
        public int AdminName { get; set; }
        public int Password { get; set; }
    }
}
