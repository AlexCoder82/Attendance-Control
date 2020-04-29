using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceControl.API.Application.Mappers
{
    public static class ShiftMapper
    {
        public static Shift Map(ShiftEntity shiftEntity)
        {
            if (shiftEntity is null)
                return null;
            else
                return new Shift()
                {
                    Id = shiftEntity.Id,
                    Description = shiftEntity.Description
            };
        }

        public static Shift MapIncludingSchedules(ShiftEntity shiftEntity)
        {
            if (shiftEntity is null)
                return null;
            else
                return new Shift()
                {
                    Id = shiftEntity.Id,
                    Description = shiftEntity.Description,
                    Schedules = shiftEntity.ScheduleEntities.Select(s=> ScheduleMapper.Map(s)).ToList()
                };
        }

        public static ShiftEntity Map(Shift shift)
        {
            if (shift is null)
                return null;
            else
            return new ShiftEntity()
            {
                Id = shift.Id,
                Description = shift.Description
            };
        }
    }
}
