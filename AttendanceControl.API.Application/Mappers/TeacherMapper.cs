using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Application.Mappers
{
    public static class TeacherMapper
    {
        public static Teacher Map(TeacherEntity teacherEntity)
        {
            return new Teacher()
            {
                Id = teacherEntity.Id,
                Dni = teacherEntity.PersonDataEntity.Dni,
                FirstName = teacherEntity.PersonDataEntity.FirstName,
                LastName1 = teacherEntity.PersonDataEntity.LastName1,
                LastName2 = teacherEntity.PersonDataEntity.LastName2,
                Username = teacherEntity.Username,
                Password = teacherEntity.Password
            };
        }

        public static TeacherEntity Map(Teacher teacher)
        {
            return new TeacherEntity()
            {
                Id = teacher.Id,
                Username = teacher.Username,
                Password = teacher.Password,
               
                PersonDataEntity = new PersonDataEntity()
                {
                    Dni = teacher.Dni,
                    FirstName = teacher.FirstName,
                    LastName1 = teacher.LastName1,
                    LastName2 = teacher.LastName2,
                }

            };
        }
    }
}
