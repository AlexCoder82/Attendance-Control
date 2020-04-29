using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface IStudentRepository
    {
        public Task<StudentEntity> Save(StudentEntity studentEntity);
        public Task<StudentEntity> Get(int studentId);
        public Task<StudentEntity> GetIncludingCourse(int studentId);
        public Task<List<StudentEntity>> GetByCourseIncludeAssignedSubjects(int courseId);
        public Task<List<StudentEntity>> GetByCourseAndSubject(int courseId,int subjectId);
        public Task<StudentEntity> GetIncludingCourseAndSubjectsAndSchoolCLasses(int studentId);
        public Task<StudentEntity> GetIncludingSubjects(int studentId);
        public Task<List<StudentEntity>> GetByPageLikeLastNameIncludingCourseAndSubjects(string lastName, int page);
        public Task<List<StudentEntity>> GetByPageIncludingCourseAndSubjects(int page);
        public Task<StudentEntity> Update(StudentEntity studentEntity);
        public Task<bool> UpdateCourse(int studentId, int courseId);
        public Task<bool> RemoveCourse(int studentId);
        public Task<bool> UpdateSubjects(int studentId,int[] subjectIds);
        public Task<bool> AddSubject(StudentEntity studentEntity, SubjectEntity subjectEntity);
        public Task<bool> RemoveSubject(StudentEntity studentEntity, SubjectEntity subjectEntity);
        public Task<StudentEntity> UpdateShoolClasses(StudentEntity studentEntity, List<SchoolClassEntity>? schoolClassEntities);
        public Task<List<StudentEntity>> GetByCourse(int courseId);
        public Task<List<StudentEntity>> GetByCurrentSchoolClass(int schoolClassId);
        public Task<List<StudentSubjectEntity>> GetAssignedSubjects(int studentId);
    }
}
