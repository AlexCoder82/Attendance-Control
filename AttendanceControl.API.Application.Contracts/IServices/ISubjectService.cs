using AttendanceControl.API.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    public interface ISubjectService
    {
        public Task<Subject> Get(int id);
        public Task<List<Subject>> GetAll();

        public Task<Subject> Save(Subject subject);

        public Task<Subject> Update(Subject subject);

        public Task<Subject> UpdateAssignedTeacher(int subjectId, int? teacherId);
    }
}
