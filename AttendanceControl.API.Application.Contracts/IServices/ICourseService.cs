
using AttendanceControl.API.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    public interface ICourseService
    {
        public Task<Course> UpdateCourse(Course course);

        public Task<List<SchoolClass>> GetSchoolClasses(int courseId);
    }
}
