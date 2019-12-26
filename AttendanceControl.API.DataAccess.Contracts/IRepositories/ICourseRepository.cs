using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface ICourseRepository :IRepository<CourseEntity>
    {
        public Task<CourseEntity> UpdateSchoolClasses(CourseEntity courseEntity);
    }
}
