using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface ISchoolClassRepository
    {
        public Task<List<SchoolClassEntity>> GetByCourse(int courseId);
        public Task<List<SchoolClassEntity>> GetByCourseAndSubject(int courseId,int subjectId);
        public Task<List<SchoolClassEntity>> GetByTeacher(int teacherId);
        public Task<SchoolClassEntity> GetCurrentIncludingSubjectAndSchedules(int schoolClassId);
        public Task<SchoolClassEntity> Get(int schoolClassId);
        public Task<SchoolClassEntity> Save(SchoolClassEntity schoolClassEntity);
        public Task<SchoolClassEntity> UpdateStudents(SchoolClassEntity schoolClassEntity, List<StudentEntity> studentEntities);
        public Task<bool> Cancel(int schoolClassId);
       
    }
}
