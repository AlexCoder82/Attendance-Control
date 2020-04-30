using AttendanceControl.API.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    /// <summary>
    ///  Contratos de la lógica relacionada con las asignaturas
    /// </summary>
    public interface ISubjectService
    {
        public Task<List<Subject>> GetAllIncludingAssignedTeacher();
        public Task<Subject> Save(Subject subject);
        public Task<Subject> Update(Subject subject);
        public Task<Subject> UpdateAssignedTeacher(int subjectId, int teacherId);
        public Task<bool> RemoveAssignedTeacher(int subjectId);
        public Task<List<Subject>> GetByCourse(int courseId);
    }
}
