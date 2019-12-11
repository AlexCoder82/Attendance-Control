using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    public class StudentEntity: PersonEntity
    {
        public int TotalAbsences { get; set; }
        public int TotalDelays { get; set; }
        public virtual ICollection<AbsenceEntity> AbsenceEntities { get; set; }
        public virtual ICollection<DelayEntity> DelayEntities { get; set; }
    }
}
