using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;

namespace AttendanceControl.API.Application.Mappers
{
    /// <summary>
    ///      Mapeos de objetos SchoolClassEntity y SchoolClass
    /// </summary>
    public class SchoolClassMapper
    {

        /// <summary>
        ///     Mapea un objeto SchoolClassEntity en un objeto SchoolClass
        ///     incluyendo
        /// </summary>
        /// <param name="schoolClassEntity"></param>
        /// <returns></returns>
        public static SchoolClass Map(SchoolClassEntity schoolClassEntity)
        {
            if (schoolClassEntity is null)
            {
                return null;
            }
            else
            {
                return new SchoolClass()
                {
                    Id = schoolClassEntity.Id,
                    Day = (DayOfWeek)schoolClassEntity.Day,
                    Subject = SubjectMapper.Map(schoolClassEntity.SubjectEntity),
                    Schedule = ScheduleMapper.Map(schoolClassEntity.ScheduleEntity)

                };
            }

        }

        /// <summary>
        ///     Mapea un objeto SchoolClass en un objeto SchoolClassEntity
        /// </summary>
        /// <param name="schoolClass"></param>
        /// <returns></returns>
        public static SchoolClassEntity Map(SchoolClass schoolClass)
        {
            return new SchoolClassEntity()
            {
                Id = schoolClass.Id,
                Day = schoolClass.Day,
                CourseId = schoolClass.Course.Id,
                SubjectId = schoolClass.Subject.Id,
                ScheduleId = schoolClass.Schedule.Id
            };
        }
      
    }
}
