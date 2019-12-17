using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Linq;


namespace AttendanceControl.API.Application.Mappers
{
    public static class StudentMapper
    {
        public static Student Map(StudentEntity studentEntity)
        {
            return new Student()
            {
                Id = studentEntity.Id,
                Dni = studentEntity.PersonDataEntity.Dni,
                FirstName = studentEntity.PersonDataEntity.FirstName,
                LastName1 = studentEntity.PersonDataEntity.LastName1,
                LastName2 = studentEntity.PersonDataEntity.LastName2,
                Course = CourseMapper.Map(studentEntity.CourseEntity),
                Subjects = studentEntity.StudentSubjectEntities
                .Select(ss=> SubjectMapper.Map(ss.SubjectEntity)).ToList(),
                TotalAbsences = studentEntity.TotalAbsences,
                TotalDelays = studentEntity.TotalDelays,
                Absences = studentEntity.AbsenceEntities.Select(a => AbsenceMapper.Map(a)).ToList()
            };
        }

        public static StudentEntity Map(Student student)
        {
            return new StudentEntity()
            {
                Id = student.Id,
                TotalAbsences = student.TotalAbsences,
                TotalDelays = student.TotalDelays,
 
                PersonDataEntity = new PersonDataEntity()
                {
                    Dni = student.Dni,
                    FirstName = student.FirstName,
                    LastName1 = student.LastName1,
                    LastName2 = student.LastName2,
                }

            };
        }
    }

}
