using AttendanceControl.API.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    public interface ISubjectService
    {
        public Task<List<Subject>> GetAllIncludingAssignedTeacher();

        public Task<Subject> Save(Subject subject);

        public Task<Subject> Update(Subject subject);

        public Task<Subject> UpdateAssignedTeacher(int subjectId, int teacherId);

        public Task<Subject> RemoveAssignedTeacher(int subjectId);

        public Task<List<Subject>> GetByCourse(int courseId);
    }
}
