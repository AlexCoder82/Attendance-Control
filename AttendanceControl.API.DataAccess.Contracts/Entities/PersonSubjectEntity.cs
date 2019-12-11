using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    public class PersonSubjectEntity
    {
        public int PersonId { get; set; }
        public int SubjectId { get; set; }
        public PersonEntity PersonEntity { get; set; }
        public SubjectEntity SubjectEntity { get; set; }
    }
}
