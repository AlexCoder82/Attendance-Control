using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    /// <summary>
    ///     Contratos del repositorio de alumnos
    /// </summary>
    public interface IStudentRepository
    {
        public Task<StudentEntity> Save(StudentEntity studentEntity);
        public Task<StudentEntity> Get(int studentId);
        public Task<StudentEntity> GetIncludingSubjects(int studentId);
        public Task<StudentEntity> GetIncludingCourseAndSubjects(int studentId);
        public Task<List<StudentEntity>> GetByPageLikeLastNameIncludingCourseAndSubjects(string lastName, int page);
        public Task<List<StudentEntity>> GetByPageIncludingCourseAndSubjects(int page);
        public Task<StudentEntity> Update(StudentEntity studentEntity);
        public Task<bool> UpdateCourse(int studentId, int courseId);
        public Task<bool> RemoveCourse(int studentId);
        public Task<bool> UpdateSubjects(int studentId,int[] subjectIds);
        public Task<List<StudentEntity>> GetByCurrentSchoolClass(int schoolClassId);
    }
}
