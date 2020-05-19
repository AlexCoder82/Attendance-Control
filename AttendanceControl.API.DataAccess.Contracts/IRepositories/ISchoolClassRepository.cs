using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    /// <summary>
    ///     Contratos del repositorio de clases
    /// </summary>
    public interface ISchoolClassRepository
    {
        public Task<List<SchoolClassEntity>> GetByCourse(int courseId);
        public Task<List<SchoolClassEntity>> GetByTeacher(int teacherId);
        public Task<SchoolClassEntity> Save(SchoolClassEntity schoolClassEntity);
        public Task<bool> Cancel(int schoolClassId);
        public Task<bool> ExistsByTeacherDayAndSchedule(int subjectId, int teacherId, DayOfWeek day, int scheduleId);

    }
}
