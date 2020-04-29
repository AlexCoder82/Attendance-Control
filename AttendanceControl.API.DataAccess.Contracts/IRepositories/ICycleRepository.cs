using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface ICycleRepository
    {
        public Task<bool> Delete(int id);
        public Task<CycleEntity> GetIncludingCoursesAndAssignedSubjects(int id);
        public Task<CycleEntity> Get(int id);
        public Task<List<CycleEntity>> GetAllIncludingCoursesSubjectsAndSchedules();
        public Task<CycleEntity> Save(CycleEntity cycleEntity);
        public Task<bool> UpdateName(int cycleId, string name);
    }
}
