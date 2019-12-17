using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceControl.API.Application.Mappers
{
    public static class CourseMapper
    {

        // Si el cliente pide un listado de los ciclos 
        public static Course Map(CourseEntity courseEntity)
        {
            return new Course()
            {
                Id = courseEntity.Id,
                Year = courseEntity.Year, 
                SchoolClasses = null,
                Subjects = courseEntity.CourseSubjectEntities
                    .Select(cs=> SubjectMapper.Map(cs.SubjectEntity)).ToList()
            };
        }

        
    }
}
