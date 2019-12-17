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
            List<CycleEntity> gradeEntities = await _cycleRepository.GetAll();

            
            List<Cycle> grades = gradeEntities.Select(g => CycleMapper.Map(g)).ToList();
            return grades;
        }

        public async Task<bool> Save(Cycle cycle)
        {
            CycleEntity cycleEntity = CycleMapper.Map(cycle);
            await _cycleRepository.Save(cycleEntity);

            return true;
        }
    }
}
