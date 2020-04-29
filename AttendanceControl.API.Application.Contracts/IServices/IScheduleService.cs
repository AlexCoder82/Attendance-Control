using AttendanceControl.API.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    public interface IScheduleService
    {
        public Task<List<Schedule>> GetByShift(int shiftId);
    }
}
