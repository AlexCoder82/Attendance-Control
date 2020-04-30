using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    /// <summary>
    ///     Contratos del repositorio de ciclos formativos
    /// </summary>
    public interface ICycleRepository
    {
        public Task<CycleEntity> GetIncludingCoursesAndAssignedSubjects(int id);
        public Task<CycleEntity> Get(int id);
        public Task<List<CycleEntity>> GetAllIncludingCoursesSubjectsAndSchedules();
        public Task<CycleEntity> Save(CycleEntity cycleEntity);
        public Task<bool> Update(CycleEntity cycleEntity);
    }
}
