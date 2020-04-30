using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.Mappers;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Services
{
    public class CycleService : ICycleService
    {
        private readonly ICycleRepository _cycleRepository;
        private readonly ILogger<CycleService> _logger;

        public CycleService(ICycleRepository cycleRepository, ILogger<CycleService> logger)
        {
            _cycleRepository = cycleRepository;
            _logger = logger;
        }

        public async Task<List<Cycle>> GetAll()
        {
            List<CycleEntity> cycleEntities = await _cycleRepository.GetAllIncludingCoursesSubjectsAndSchedules();

            List<Cycle> cycles = cycleEntities.Select(c => new Cycle
            {
                Id = c.Id,
                Name = c.Name,
                Courses = c.CourseEntities.Select(co => new Course
                {
                    Id = co.Id,
                    Year = co.Year
                }).ToList(),
                Shift = new Shift
                {
                    Id = c.ShiftEntity.Id,
                    Description = c.ShiftEntity.Description,
                    Schedules = c.ShiftEntity.ScheduleEntities.Select(s => new Schedule
                    {
                        Id = s.Id,
                        Start = s.Start.ToString(@"hh\:mm"),
                        End = s.End.ToString(@"hh\:mm"),
                    }).ToList()
                }

            }).ToList();

            
            return cycles;
        }

        public async Task<Cycle> Save(Cycle cycle)
        {
     
            CycleEntity cycleEntity = CycleMapper.Map(cycle);
            cycleEntity = await _cycleRepository.Save(cycleEntity);

            cycleEntity = await _cycleRepository.GetIncludingCoursesAndAssignedSubjects(cycleEntity.Id);
 
            cycle = CycleMapper.MapIncludingCourses(cycleEntity);

            return cycle;
        }

        public async Task<bool> Update(Cycle cycle)
        {
            CycleEntity cycleEntity = CycleMapper.Map(cycle);

            return await _cycleRepository.Update(cycleEntity);
        }
    }
}
