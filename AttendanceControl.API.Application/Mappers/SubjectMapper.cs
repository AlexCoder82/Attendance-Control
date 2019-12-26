using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Linq;

namespace AttendanceControl.API.Application.Mappers
{
    public static class SubjectMapper
    {
        public static Subject MapIncludingTeacher(SubjectEntity subjectEntity)
        {
            if (subjectEntity is null)
                return null;
            else
                return new Subject()
                {
                    Id = subjectEntity.Id,
                    Name = subjectEntity.Name,
                    Teacher = TeacherMapper.Map(subjectEntity.TeacherEntity)

                };
        }

        public static Subject Map(SubjectEntity subjectEntity)
        {
            if (subjectEntity is null)
                return null;
            else
                return new Subject()
                {
                    Id = subjectEntity.Id,
                    Name = subjectEntity.Name
                };
        }

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
