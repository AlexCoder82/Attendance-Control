using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface IScheduleRepository: IRepository<ScheduleEntity>
    {
    }
}
