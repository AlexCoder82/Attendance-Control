using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    public class CycleSubjectEntity
    {
        public int CycleId { get; set; }
        public int SubjectId { get; set; }
        public CycleEntity CycleEntity { get; set; }
        public SubjectEntity SubjectEntity { get; set; }

    }
}
