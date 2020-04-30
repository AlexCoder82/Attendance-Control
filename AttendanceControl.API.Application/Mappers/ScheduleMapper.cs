using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;

namespace AttendanceControl.API.Application.Mappers
{
    /// <summary>
    ///     Mapeos de objetos ScheduleEntity y Schedule
    /// </summary>
    /// <param name="scheduleEntity"></param>
    /// <returns></returns>
    public static class ScheduleMapper
    {

        /// <summary>
        ///     Mapea un objeto ScheduleEntity en un objeto Schedule
        /// </summary>
        /// <param name="scheduleEntity"></param>
        /// <returns></returns>
        public static Schedule Map(ScheduleEntity scheduleEntity)
        {

            return new Schedule()
            {
                Id = scheduleEntity.Id,
                Start = scheduleEntity.Start.ToString(@"hh\:mm"),
                End = scheduleEntity.End.ToString(@"hh\:mm")
            };

        }

    }
}
