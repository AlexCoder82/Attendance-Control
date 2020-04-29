using AttendanceControl.API.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    public interface IStudentService
    {
        public Task<Student> Save(Student studen);

        public Task<List<Student>> GetByPageIncludingCourseAndSubjects(int page);
        public Task<List<Student>> GetByPageLikeLastNameIncludingCourseAndSubjects(string lastName, int page);

        public Task<Student> Update(Student student);

        public Task<Student> UpdateCourse(int studentId, int courseId);

        public Task<bool> RemoveCourse(int studentId);

        public Task<Student> UpdateSubjects(int studentId, int[] subjectIds);

        public Task<List<Student>> GetByCourse(int courseId);
    }
}
