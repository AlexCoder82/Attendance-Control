using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Linq;

namespace AttendanceControl.API.Application.Mappers
{
    /// <summary>
    ///     Mapeos entre objetos CourseEntity y Course
    /// </summary>
    public static class CourseMapper
    {

        /// <summary>
        ///     Mapea un objeto CourseEntity en un objeto Course
        ///     incluyendo las asignaturas, las cuales incluyen el 
        ///     profesor asignado
        /// </summary>
        /// <param name="courseEntity"></param>
        /// <returns></returns>
        public static Course MapIncludingSubjects(CourseEntity courseEntity)
        {
            if (courseEntity is null)
            {
                return null;
            }
            else
            {
                return new Course()
                {
                    Id = courseEntity.Id,
                    Year = courseEntity.Year,
                    SchoolClasses = null,
                    Subjects = courseEntity.CourseSubjectEntities
                    .Select(cs => SubjectMapper.MapIncludingTeacher(cs.SubjectEntity)).ToList()
                };
            }
        }


        /// <summary>
        ///     Mapea un objeto CourseEntity en un objeto Course
        ///     incluyendo el ciclo al que pertenece
        /// </summary>
        /// <param name="courseEntity"></param>
        /// <returns></returns>
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
                    Cycle = CycleMapper.Map(courseEntity.CycleEntity)

                };
        }

        /// <summary>
        ///     Mapea un objeto Course en un objeto CourseEntity
        ///     incluyendo las relaciones con las asignaturas
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public static CourseEntity MapIncludingSubjects(Course course)
        {
            if (course is null)
            {
                return null;
            }
            else
            {
                return new CourseEntity()
                {
                    Id = course.Id,
                    Year = course.Year,
                    SchoolClassEntities = null,
                    CourseSubjectEntities = course.Subjects
                        .Select(s => new CourseSubjectEntity()
                        {
                            CourseId = course.Id,
                            SubjectId = s.Id,

                        }).ToList()
                };
            }
        }
       

    }
}
