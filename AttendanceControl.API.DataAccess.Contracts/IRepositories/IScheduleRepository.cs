using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface IScheduleRepository
    {
        public  Task<List<ScheduleEntity>> GetByShift(int ShiftId);
    }
}
