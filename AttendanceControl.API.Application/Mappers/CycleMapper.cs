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
                CourseEntities = cycle.Courses.Select(c=> new CourseEntity
                {
                    Year = c.Year
                }).ToList(),
                ShiftId = cycle.Shift.Id
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
                    Courses = cycleEntity.CourseEntities.Select(c=>CourseMapper
                    .MapIncludingSubjects(c)).ToList(),
                    Shift = ShiftMapper.Map(cycleEntity.ShiftEntity)
                };
        }

        public static Cycle MapIncludingCoursesAndSchedules(CycleEntity cycleEntity)
        {
            if (cycleEntity is null)
                return null;
            else
                return new Cycle()
                {
                    Id = cycleEntity.Id,
                    Name = cycleEntity.Name,
                    Courses = cycleEntity.CourseEntities.Select(c => new Course
                    {
                        Id = c.Id,
                        Year = c.Year
                    }).ToList(),
                    Shift = ShiftMapper.MapIncludingSchedules(cycleEntity.ShiftEntity)
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
