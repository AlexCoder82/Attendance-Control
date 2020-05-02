using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Collections.Generic;
using System.Linq;


namespace AttendanceControl.API.Application.Mappers
{
    /// <summary>
    ///     Mapeos entre objetos CycleEntity y Cycle
    /// </summary>
    public static class CycleMapper
    {

        /// <summary>
        ///     Mapea un objeto Cycle en un objeto CycleEntity
        ///     incluyendo los cursos 
        /// </summary>
        /// <param name="cycle"></param>
        /// <returns></returns>
        public static CycleEntity Map(Cycle cycle)
        {
            return new CycleEntity()
            {
                Id = cycle.Id,
                Name = cycle.Name,
                CourseEntities = cycle.Courses.Select(c => new CourseEntity
                {
                    Year = c.Year
                }).ToList(),
                ShiftId = cycle.Shift.Id,
                ShiftEntity = ShiftMapper.Map(cycle.Shift)
            };
        }

        /// <summary>
        ///     Mapea un objeto CycleEntity en un objeto Cycle
        ///     incluyendo el turno del ciclo, los cursos y las
        ///     asignaturas del los cursos
        /// </summary>
        /// <param name="cycleEntity"></param>
        /// <returns></returns>
        public static Cycle MapIncludingCourses(CycleEntity cycleEntity)
        {
            if (cycleEntity is null)
            {
                return null;
            }
            else
            {
                return new Cycle()
                {
                    Id = cycleEntity.Id,
                    Name = cycleEntity.Name,
                    Courses = cycleEntity.CourseEntities.Select(c => CourseMapper
                    .MapIncludingSubjects(c)).ToList(),
                    Shift = ShiftMapper.Map(cycleEntity.ShiftEntity)
                };
            }
        }

        /// <summary>
        ///     Mapea un objeto CycleEntity en un objeto Cycle
        ///     incluyendo el turno,los horarios del turno y los cursos
        /// </summary>
        /// <param name="cycleEntity"></param>
        /// <returns></returns>
        public static Cycle MapIncludingCoursesAndSchedules(CycleEntity cycleEntity)
        {

            if (cycleEntity is null)
            {
                return null;
            }
            else
            {
                return new Cycle
                {
                    Id = cycleEntity.Id,
                    Name = cycleEntity.Name,
                    Courses = cycleEntity.CourseEntities
                        .Select(co => new Course
                        {
                            Id = co.Id,
                            Year = co.Year
                        }).ToList(),
                    Shift = new Shift
                    {
                        Id = cycleEntity.ShiftEntity.Id,
                        Description = cycleEntity.ShiftEntity.Description,
                        Schedules = cycleEntity.ShiftEntity.ScheduleEntities.Select(s => new Schedule
                        {
                            Id = s.Id,
                            Start = s.Start.ToString(@"hh\:mm"),
                            End = s.End.ToString(@"hh\:mm"),
                        }).ToList()
                    }
                };
            }

        }


        /// <summary>
        ///     Mapea un objeto CycleEntity en un objeto Cycle
        /// </summary>
        /// <param name="cycleEntity"></param>
        /// <returns></returns>
        public static Cycle Map(CycleEntity cycleEntity)
        {
            if (cycleEntity is null)
            {
                return null;
            }
            else
            {
                return new Cycle()
                {
                    Id = cycleEntity.Id,
                    Name = cycleEntity.Name

                };
            }
        }

    }
}
