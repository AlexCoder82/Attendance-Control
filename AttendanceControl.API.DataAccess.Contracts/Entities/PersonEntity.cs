using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    public class PersonEntity
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public virtual ICollection<SubjectEntity> SubjectEntities {get;set;}
    }
}
