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
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ILogger<ScheduleService> _logger;

        public ScheduleService(IScheduleRepository scheduleRepository, ILogger<ScheduleService> logger)
        {
            _scheduleRepository = scheduleRepository;
            _logger = logger;
        }

      
        public async Task<List<Schedule>> GetAll()
        {
            List<ScheduleEntity> scheduleEntities = await _scheduleRepository.GetAll();


            List<Schedule> schedules = scheduleEntities.Select(s => ScheduleMapper.Map(s)).ToList();

            return schedules;
        }
    }
}
