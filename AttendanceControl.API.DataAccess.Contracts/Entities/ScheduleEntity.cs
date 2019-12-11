using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    public class ScheduleEntity
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int SubjectId { get; set; }
        public SubjectEntity SubjectEntity { get; set; }
    }
}
