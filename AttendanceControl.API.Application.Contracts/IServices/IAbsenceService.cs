using AttendanceControl.API.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    /// <summary>
    ///  Contratos de la lógica relacionada con las ausencias
    /// </summary>
    public interface IAbsenceService
    {
        public Task<List<Absence>> GetByStudent(int studentId);
        public Task<bool> SetExcused(int absenceId, bool isExcused);
    }
}
