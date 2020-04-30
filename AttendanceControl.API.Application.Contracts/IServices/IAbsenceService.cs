using AttendanceControl.API.Application.DTOs;
using AttendanceControl.API.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    public interface IAbsenceService
    {
        public Task<List<Absence>> GetByStudent(int studentId);
        public Task<bool> SetExcused(int absenceId, bool isExcused);
    }
}
