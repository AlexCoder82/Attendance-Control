using AttendanceControl.API.Application.DTOs;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;

namespace AttendanceControl.API.Application.Mappers
{
    public static class AbsenceMapper
    {
        public static Absence MapIncludingSchedule(AbsenceEntity absenceEntity)
        {
            if (absenceEntity is null)
            {
                return null;
            }
            else
                return new Absence()
                {
                    Id = absenceEntity.Id,
                    Type = absenceEntity.Type,
                    Date = absenceEntity.Date,
                    Schedule = ScheduleMapper.Map(absenceEntity.SchoolClassEntity.ScheduleEntity),
                    Subject = SubjectMapper.Map(absenceEntity.SchoolClassEntity.SubjectEntity),
                    IsExcused = absenceEntity.IsExcused

                };
        }
        public static Absence Map(AbsenceEntity absenceEntity)
        {
            if (absenceEntity is null)
            {
                return null;
            }
            else
                return new Absence()
                {
                    Id = absenceEntity.Id,
                    Type = absenceEntity.Type,
                    Date = absenceEntity.Date

                };
        }

        public static AbsenceEntity Map(AbsenceDto createAbsenceDto)
        {
            return new AbsenceEntity()
            {
                Type = createAbsenceDto.Type,
                Date = DateTime.Today,
                SchoolClassId = createAbsenceDto.SchoolClassId,
                StudentId = createAbsenceDto.StudentId
            };
        }
    }
}
