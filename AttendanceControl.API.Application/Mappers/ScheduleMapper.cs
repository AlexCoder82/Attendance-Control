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
                Start = scheduleEntity.Start.ToString(@"hh\:mm"),
                End = scheduleEntity.End.ToString(@"hh\:mm")
        };
        }

        public static ScheduleEntity Map(Schedule schedule)
        {
            return new ScheduleEntity()
            {
                Id = schedule.Id,
                Start = TimeSpan.Parse(schedule.Start),
                End = TimeSpan.Parse(schedule.End)
            };
        }
    }
}
