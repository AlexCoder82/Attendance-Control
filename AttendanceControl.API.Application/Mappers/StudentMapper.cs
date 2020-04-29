using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Linq;


namespace AttendanceControl.API.Application.Mappers
{
    public static class StudentMapper
    {

        public static Student MapIncludingCourse(StudentEntity studentEntity)
        {
            if (studentEntity is null)
            {
                return null;
            }
            else
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

        public static Student Map(StudentEntity studentEntity)
        {
            if (studentEntity is null)
            {
                return null;
            }
            else
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

        public static Student MapIncludingAssignedSubjects(StudentEntity studentEntity)
        {
            if (studentEntity is null)
            {
                return null;
            }
            else
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
