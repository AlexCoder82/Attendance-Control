using AttendanceControl.API.Application.Contracts.DTOs;
using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceControl.API.Application.Mappers
{
    public class SchoolClassMapper
    {
        public static SchoolClass Map(SchoolClassEntity schoolClassEntity)
        {
            return new SchoolClass()
            {
                Id = schoolClassEntity.Id,
                Day = (DayOfWeek)schoolClassEntity.Day,
                Subject = SubjectMapper.Map(schoolClassEntity.SubjectEntity),
                Schedule = ScheduleMapper.Map(schoolClassEntity.ScheduleEntity)
            };
        }

        public static SchoolClass MapIncludingStudents(SchoolClassEntity schoolClassEntity)
        {

            return new SchoolClass()
            {
                Id = schoolClassEntity.Id,
                Day = (DayOfWeek)schoolClassEntity.Day,
                Students = schoolClassEntity.SchoolClassStudentEntities
                    .Select(scs => StudentMapper.Map(scs.StudentEntity)).ToList()
            };
        }

        public static SchoolClass MapIncludingSubjectScheduleAndStudents(SchoolClassEntity schoolClassEntity)
        {
            if (schoolClassEntity is null)
            {
                return null;
            }
            else
            {

                return new SchoolClass()
                {
                    Id = schoolClassEntity.Id,
                    Day = (DayOfWeek)schoolClassEntity.Day,
                    Subject = SubjectMapper.Map(schoolClassEntity.SubjectEntity),
                    Schedule = ScheduleMapper.Map(schoolClassEntity.ScheduleEntity)

                };
            }

        }

        public static SchoolClassEntity Map(SchoolClass schoolClass)
        {
            return new SchoolClassEntity()
            {
                Id = schoolClass.Id,
                Day = schoolClass.Day,
                CourseId = schoolClass.Course.Id,
                SubjectId = schoolClass.Subject.Id,
                ScheduleId = schoolClass.Schedule.Id
            };
        }

      
    }
}
