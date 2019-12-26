using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<ScheduleRepository> _logger;

        public ScheduleRepository(IAttendanceControlDBContext dbContext, ILogger<ScheduleRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ScheduleEntity> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ScheduleEntity>> GetAll()
        {
            return await _dbContext.ScheduleEntities.ToListAsync();
        }

        public Task<ScheduleEntity> Save(ScheduleEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<ScheduleEntity> Update(ScheduleEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
