using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    /// <summary>
    ///     Contratos del repositorio de administradores
    /// </summary>
    public interface IAdminRepository
    {
        public Task<AdminEntity> Exists(AdminEntity adminEntity);
    }
}
