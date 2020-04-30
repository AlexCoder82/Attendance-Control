using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;

namespace AttendanceControl.API.Application.Mappers
{
    /// <summary>
    ///     Mapeos entre objetos AbsenceEntity y Absence
    /// </summary>
    public static class AbsenceMapper
    {
        /// <summary>
        ///     Mapea una entidad AbsenceEntity en un objeto Absence
        ///     incluyendo el objeto Schedule y el objeto Subject
        /// </summary>
        /// <param name="absenceEntity"></param>
        /// <returns></returns>
        public static Absence MapIncludingSchedule(AbsenceEntity absenceEntity)
        {
            if (absenceEntity is null)
            {
                return null;
            }
            else
            {
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
        }

        /// <summary>
        ///     Mapea un objeto AbsenceEntity en un objeto Absence
        /// </summary>
        /// <param name="absenceEntity"></param>
        /// <returns></returns>
        public static Absence Map(AbsenceEntity absenceEntity)
        {
            if (absenceEntity is null)
            {
                return null;
            }
            else
            {
                return new Absence()
                {
                    Id = absenceEntity.Id,
                    Type = absenceEntity.Type,
                    Date = absenceEntity.Date
                };
            }
        }

    }
}
