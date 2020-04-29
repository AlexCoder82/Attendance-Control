using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Linq;

namespace AttendanceControl.API.Application.Mappers
{
    public static class CourseMapper
    {

        // Si el cliente pide un listado de los ciclos 
        public static Course MapIncludingSubjects(CourseEntity courseEntity)
        {
            if (courseEntity is null)
                return null;
            else
                return new Course()
            {
                Id = courseEntity.Id,
                Year = courseEntity.Year, 
                SchoolClasses = null,
                Subjects = courseEntity.CourseSubjectEntities
                    .Select(cs=> SubjectMapper.MapIncludingTeacher(cs.SubjectEntity)).ToList()
            };
        }

        
        // Si el cliente pide un listado de las a 
        public static Course MapIncludingCycle(CourseEntity courseEntity)
        {
            if (courseEntity is null)
                return null;
            else
            return new Course()
            {
                Id = courseEntity.Id,
                Year = courseEntity.Year,
                SchoolClasses = null,
                Cycle = CycleMapper.Map( courseEntity.CycleEntity)
 
            };
        }

        // Si el cliente pide actualizar un curso 
        public static CourseEntity MapIncludingSubjects(Course course)
        {
            if (course is null)
                return null;
            else
                return new CourseEntity()
            {
                Id = course.Id,
                Year = course.Year,
                SchoolClassEntities = null,
                CourseSubjectEntities = course.Subjects.Select(s=>new CourseSubjectEntity()
                {
                    CourseId = course.Id,
                    SubjectId = s.Id,
                    
                })
                .ToList()
            };
        }

        // Si el cliente pide actualizar un curso 
        public static CourseEntity Map(Course course)
        {
            if (course is null)
                return null;
            else
                return new CourseEntity()
            {
                Id = course.Id,
                Year = course.Year,
                CycleEntity = CycleMapper.Map(course.Cycle)
            };
        }

    }
}
