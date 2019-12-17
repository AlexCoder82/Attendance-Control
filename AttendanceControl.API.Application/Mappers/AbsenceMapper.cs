
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;


namespace AttendanceControl.API.Application.Mappers
{
    public static class AbsenceMapper
    {
        public static Absence Map(AbsenceEntity absenceEntity)
        {
            return new Absence()
            {
                Id = absenceEntity.Id,
                Type = (AttendanceControl.API.Business.Enums.AbsenceType)absenceEntity.Type,
                Date = absenceEntity.Date,
                Schedule = ScheduleMapper.Map(absenceEntity.SchoolClassEntity.ScheduleEntity),
                Subject = SubjectMapper.Map(absenceEntity.SchoolClassEntity.SubjectEntity)              
            };
        }
    }
}
