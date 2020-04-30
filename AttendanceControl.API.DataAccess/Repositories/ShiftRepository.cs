using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    /// <summary>
    ///     Repositorio de turnos
    /// </summary>
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

        /// <summary>
        ///     Obtiene la lista de entidades turno 
        /// </summary>
        /// <returns>
        ///     Returna la lista de entidades turno
        /// </returns>
        public async Task<List<ShiftEntity>> GetAll()
        {

            return  await _dbContext.ShiftEntities.ToListAsync();

            
        }
    }
}
