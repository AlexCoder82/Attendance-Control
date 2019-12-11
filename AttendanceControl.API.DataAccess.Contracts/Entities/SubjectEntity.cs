using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    public class SubjectEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CycleSubjectEntity> CycleSubjectEntities { get; set; }
    }
}
