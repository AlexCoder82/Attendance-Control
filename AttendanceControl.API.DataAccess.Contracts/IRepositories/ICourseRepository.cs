using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface ICourseRepository 
    {
        public Task<bool> AssignSubject(int courseId, int subjectID);
        public Task<bool> RenoveAssignedSubject(int courseId, int subjectId);
        public Task<CourseEntity> GetIncludingAssignedSubjects(int courseId);
        public  Task<CourseEntity> GetIncludingCycleAndAssignedSubjects(int courseId);
        public Task<List<CourseEntity>> GetAll();
    }
}
