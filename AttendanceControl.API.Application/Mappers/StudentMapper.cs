using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Linq;


namespace AttendanceControl.API.Application.Mappers
{
    /// <summary>
    ///     Mapeos de objetos StudentEntity y Student
    /// </summary>
    public static class StudentMapper
    {

        /// <summary>
        ///     Mapea un objeto StudentEntity en un objeto Student
        ///     incluyendo el curso cursado por el alumno
        /// </summary>
        /// <param name="studentEntity"></param>
        /// <returns></returns>
        public static Student MapIncludingCourse(StudentEntity studentEntity)
        {
            if (studentEntity is null)
            {
                return null;
            }
            else
            {
                return new Student()
                {
                    Id = studentEntity.Id,
                    Dni = studentEntity.Dni,
                    FirstName = studentEntity.FirstName,
                    LastName1 = studentEntity.LastName1,
                    LastName2 = studentEntity.LastName2,
                    Course = CourseMapper.MapIncludingCycle(studentEntity.CourseEntity),
                    TotalAbsences = studentEntity.TotalAbsences,
                    TotalDelays = studentEntity.TotalDelays

                };
            }
        }

        /// <summary>
        ///     Mapea un objeto StudentEntity en un objeto Student
        /// </summary>
        /// <param name="studentEntity"></param>
        /// <returns></returns>
        public static Student Map(StudentEntity studentEntity)
        {

            if (studentEntity is null)
            {
                return null;
            }
            else
            {
                return new Student()
                {
                    Id = studentEntity.Id,
                    Dni = studentEntity.Dni,
                    FirstName = studentEntity.FirstName,
                    LastName1 = studentEntity.LastName1,
                    LastName2 = studentEntity.LastName2,
                    TotalAbsences = studentEntity.TotalAbsences,
                    TotalDelays = studentEntity.TotalDelays

                };
            }

        }

        /// <summary>
        ///     Mapea un objeto StudentEntity en un objeto Student
        ///     incluyendo el curso que cursa y las asignaturas que cursa
        ///     el alumno
        /// </summary>
        /// <param name="studentEntity"></param>
        /// <returns></returns>
        public static Student MapIncludingAssignedCourseAndSubjects(StudentEntity studentEntity)
        {
            if (studentEntity is null)
            {
                return null;
            }
            else
            {
                return new Student()
                {
                    Id = studentEntity.Id,
                    Dni = studentEntity.Dni,
                    FirstName = studentEntity.FirstName,
                    LastName1 = studentEntity.LastName1,
                    LastName2 = studentEntity.LastName2,
                    Course = CourseMapper.MapIncludingCycle(studentEntity.CourseEntity),
                    Subjects = studentEntity.StudentSubjectEntities
                    .Select(ss => SubjectMapper.Map(ss.SubjectEntity)).ToList(),
                    TotalAbsences = studentEntity.TotalAbsences,
                    TotalDelays = studentEntity.TotalDelays

                };
            }

        }

        /// <summary>
        ///     Mapea un objeto Student en un objeto StudentEntity
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public static StudentEntity Map(Student student)
        {

            return new StudentEntity()
            {
                Id = student.Id,
                TotalAbsences = student.TotalAbsences,
                TotalDelays = student.TotalDelays,
                Dni = student.Dni,
                FirstName = student.FirstName,
                LastName1 = student.LastName1,
                LastName2 = student.LastName2,
            };

        }
    }

}
