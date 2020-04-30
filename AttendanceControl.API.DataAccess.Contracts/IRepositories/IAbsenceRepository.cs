using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    /// <summary>
    ///     Contratos del repositorio de ausencias
    /// </summary>
    public interface IAbsenceRepository
    {
        public Task<AbsenceEntity> Get(int absenceId);
        public Task<List<AbsenceEntity>> GetByStudent(int studentId);
        public Task<AbsenceEntity> GetByStudentAndSchoolClass(int studentId,int schoolClassId);
        public Task<bool> Save(List<AbsenceEntity> absenceEntities);
        public Task<bool> SetExcused(AbsenceEntity absenceEntity, bool isExcused);  
    }
}
