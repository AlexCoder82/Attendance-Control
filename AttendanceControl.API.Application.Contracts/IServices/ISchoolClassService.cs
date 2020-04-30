using AttendanceControl.API.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    /// <summary>
    ///  Contratos de la lógica relacionada con las clases
    /// </summary>
    public interface ISchoolClassService
    {
        public Task<SchoolClass> Save( SchoolClass schoolClass);
        public Task<bool> Cancel(int schoolClassId); 
        public Task<List<SchoolClass>> GetByCourse(int courseId);
        public Task<List<SchoolClass>> GetByTeacher(int teacherId);
    }
}
