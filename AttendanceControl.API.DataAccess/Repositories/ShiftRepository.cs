using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<ShiftRepository> _logger;

        public ShiftRepository(IAttendanceControlDBContext dbContext,
                                 ILogger<ShiftRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<ShiftEntity>> GetAll()
        {
            return  await _dbContext.ShiftEntities.ToListAsync();
            
        }
    }
}
