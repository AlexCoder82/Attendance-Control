using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttendanceControl.API.Application.Mappers
{
    public static class CycleMapper
    {
        public static CycleEntity Map(Cycle cycle)
        {
            return new CycleEntity()
            {
                Id= cycle.Id,
                Name = cycle.Name,
                CourseEntities =  new List<CourseEntity>()
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

        public static Cycle Map(CycleEntity cycleEntity)
        {
            return new Cycle()
            {
                Id = cycleEntity.Id,
                Name = cycleEntity.Name,
                Courses = cycleEntity.CourseEntities.Select(c => CourseMapper.Map(c)).ToArray()
            };
        }
    }
}
