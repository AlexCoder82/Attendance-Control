using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface ISubjectRepository: IRepository<SubjectEntity>
    {
       // public Task<SubjectEntity> GetIncludingTeacher(int subjectId);
        public Task<SubjectEntity> UpdateAssignedTeacher(int subjectId, int? teacherId);
    }
}
