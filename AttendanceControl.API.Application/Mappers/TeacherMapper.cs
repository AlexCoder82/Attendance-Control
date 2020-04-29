using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace AttendanceControl.API.Application.Mappers
{
    public static class TeacherMapper
    {
        public static Teacher Map(TeacherEntity teacherEntity)
        {

            if (teacherEntity is null)
            {
                return null;
            }
            else
                return new Teacher()
                {
                    Id = teacherEntity.Id,
                    Dni = teacherEntity.Dni,
                    FirstName = teacherEntity.FirstName,
                    LastName1 = teacherEntity.LastName1,
                    LastName2 = teacherEntity.LastName2
                };
        }

        public static Teacher MapIncludingCredentials(TeacherEntity teacherEntity)
        {

            if (teacherEntity is null)
            {
                return null;
            }
            else
                return new Teacher()
                {
                    Id = teacherEntity.Id,
                    Dni = teacherEntity.Dni,
                    FirstName = teacherEntity.FirstName,
                    LastName1 = teacherEntity.LastName1,
                    LastName2 = teacherEntity.LastName2,
                    Username = teacherEntity.TeacherCredentialsEntity.Username,
                    Password = teacherEntity.TeacherCredentialsEntity.Password
                };
        }

        public static TeacherEntity Map(Teacher teacher)
        {
            return new TeacherEntity()
            {
                Id = teacher.Id,

                Dni = teacher.Dni,
                FirstName = teacher.FirstName,
                LastName1 = teacher.LastName1,
                LastName2 = teacher.LastName2,


            };
        }
    }
}
