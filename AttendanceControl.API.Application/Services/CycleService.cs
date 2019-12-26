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

        public async Task<bool> Delete(int id)
        {
           return await _cycleRepository.Delete(id);
        }

        public async Task<List<Cycle>> GetAll()
        {
            List<CycleEntity> gradeEntities = await _cycleRepository.GetAll();

            
            List<Cycle> grades = gradeEntities.Select(g => CycleMapper.MapIncludingCourses(g)).ToList();
            return grades;
        }

        public async Task<Cycle> Save(Cycle cycle)
        {
            CycleEntity cycleEntity = CycleMapper.Map(cycle);
            cycleEntity = await _cycleRepository.Save(cycleEntity);

            cycleEntity = await _cycleRepository.Get(cycleEntity.Id);
           // Console.WriteLine("\n\nAAAAAAAAAAAAAAAAAAAAAAAAAA" +cycleEntity.Id);
            cycle = CycleMapper.Map(cycleEntity);

            return cycle;
        }

        public async Task<Cycle> Update(Cycle cycle)
        {
            CycleEntity cycleEntity = CycleMapper.Map(cycle);
            cycleEntity = await _cycleRepository.Update(cycleEntity);

            cycleEntity = await _cycleRepository.Get(cycleEntity.Id);
            // Console.WriteLine("\n\nAAAAAAAAAAAAAAAAAAAAAAAAAA" +cycleEntity.Id);
            cycle = CycleMapper.Map(cycleEntity);

            return cycle;
        }
    }
}
