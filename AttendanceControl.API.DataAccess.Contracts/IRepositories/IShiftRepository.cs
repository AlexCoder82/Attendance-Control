using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    /// <summary>
    ///     Contratos del repositorio de turnos de horarios
    /// </summary>
    public interface IShiftRepository
    {
        public Task<List<ShiftEntity>> GetAll();
    }
}
