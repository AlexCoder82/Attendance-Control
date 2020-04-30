using AttendanceControl.API.Application.Contracts.DTOs;
using AttendanceControl.API.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    /// <summary>
    ///  Contratos de la lógica relacionada con los profesores
    /// </summary>
    public interface ITeacherService
    {
        public Task<List<Teacher>> GetAll();
        public Task<Teacher> Update(Teacher teacher);
        public Task<Teacher> Save(Teacher teacher);
        public Task<TeacherSignInResponse> SignIn(string dni);
    }
}
