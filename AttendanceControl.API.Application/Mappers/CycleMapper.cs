using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Collections.Generic;
using System.Linq;


namespace AttendanceControl.API.Application.Mappers
{
    public static class CycleMapper
    {
        public static CycleEntity Map(Cycle cycle)
        {
            return new CycleEntity()
            {
                Id = cycle.Id,
                Name = cycle.Name,
                CourseEntities = new List<CourseEntity>()
                {
                    new CourseEntity()
                    {
                        Year =1
                    },
                    new CourseEntity()
                    {
                        Year =2
                    }
                }
            };
        }

        public static Cycle MapIncludingCourses(CycleEntity cycleEntity)
        {
            if (cycleEntity is null)
                return null;
            else
                return new Cycle()
                {
                    Id = cycleEntity.Id,
                    Name = cycleEntity.Name,
                    FirstCourse = CourseMapper
                    .MapIncludingSubjects(cycleEntity.CourseEntities.FirstOrDefault(c => c.Year == 1)),
                    SecondCourse = CourseMapper
                    .MapIncludingSubjects(cycleEntity.CourseEntities.FirstOrDefault(c => c.Year == 2))
                };
        }

        public static Cycle Map(CycleEntity cycleEntity)
        {
            if (cycleEntity is null)
                return null;
            else
                return new Cycle()
                {
                    Id = cycleEntity.Id,
                    Name = cycleEntity.Name

                };
        }
    }
}
