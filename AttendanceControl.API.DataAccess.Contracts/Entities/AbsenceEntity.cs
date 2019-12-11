using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    public class AbsenceEntity
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int StudentId { get; set; }
        public ScheduleEntity ScheduleEntity { get; set; }
        public StudentEntity StudentEntity { get; set; }
    }
}
