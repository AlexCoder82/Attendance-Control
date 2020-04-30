using AttendanceControl.API.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    /// <summary>
    ///  Contratos de la lógica relacionada con los turnos de horarios
    public interface IShiftService
    {
        public Task<List<Shift>> GetAll();
    }
}
