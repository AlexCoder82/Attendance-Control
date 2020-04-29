using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface ISubjectRepository
    {
      
        public Task<SubjectEntity> UpdateAssignedTeacher(int subjectId, TeacherEntity? teacherEntity);

        public Task<SubjectEntity> GetIncludingAssignedTeacher(int id);

        public Task<SubjectEntity> Get(int id);

        public Task<List<SubjectEntity>> GetByCourse(int courseId);

        public Task<List<SubjectEntity>> GetAllIncludingAssignedTeacher();   

        public Task<SubjectEntity> Save(SubjectEntity entity);

        public Task<SubjectEntity> Update(SubjectEntity entity);

    }
}
