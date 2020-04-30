using AttendanceControl.API.Application.Contracts.DTOs;
using AttendanceControl.API.Business.Models;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    /// <summary>
    ///  Contratos de la lógica relacionada con las administradores
    /// </summary>
    public interface IAdminService
    {
        public Task<AdminSignInResponse> SignIn(Admin admin);
    }
}
