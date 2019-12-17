using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceControl.API.Application.Mappers
{
    public static class ScheduleMapper
    {
        public static Schedule Map(ScheduleEntity scheduleEntity)
        {
            return new Schedule()
            {
                Id = scheduleEntity.Id,
                Day = (AttendanceControl.API.Business.Enums.Day)scheduleEntity.Day,
                Start = scheduleEntity.Start,
                End = scheduleEntity.End
            };
        }

        public static ScheduleEntity Map(Schedule schedule)
        {
            return new ScheduleEntity()
            {
                Id = schedule.Id,
                Day = (AttendanceControl.API.DataAccess.Contracts.Enums.Day)schedule.Day,
                Start = schedule.Start,
                End = schedule.End
            };
        }
    }
}
