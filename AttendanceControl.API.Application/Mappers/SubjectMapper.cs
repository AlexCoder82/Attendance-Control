using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Linq;

namespace AttendanceControl.API.Application.Mappers
{
    /// <summary>
    ///     Mapeo entre objetos SubjectEntity y Subject
    /// </summary>
    public static class SubjectMapper
    {

        /// <summary>
        ///     Mapea un objeto SubjectEntity en un objeto Subject
        ///     incluyendo el profesor asignado
        /// </summary>
        /// <param name="subjectEntity"></param>
        /// <returns></returns>
        public static Subject MapIncludingTeacher(SubjectEntity subjectEntity)
        {

            if (subjectEntity is null)
            {
                return null;
            }
            else
            {
                return new Subject()
                {
                    Id = subjectEntity.Id,
                    Name = subjectEntity.Name,
                    Teacher = TeacherMapper.Map(subjectEntity.TeacherEntity)
                };
            }

        }

        /// <summary>
        ///     Mapea un objeto SubjectEntity en un objeto Subject
        /// </summary>
        /// <param name="subjectEntity"></param>
        /// <returns></returns>
        public static Subject Map(SubjectEntity subjectEntity)
        {
            if (subjectEntity is null)
            {
                return null;
            }
            else
            {
                return new Subject()
                {
                    Id = subjectEntity.Id,
                    Name = subjectEntity.Name
                };
            }

        }

        /// <summary>
        ///     Mapea un objeto Subject en un objeto SubjectEntity
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static SubjectEntity Map(Subject subject)
        {

            return new SubjectEntity()
            {
                Id = subject.Id,
                Name = subject.Name
            };

        }

    }
}
