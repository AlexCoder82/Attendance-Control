using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    /// <summary>
    ///     Clase que contiene los métodos de acceso a la tabla de horarios
    /// </summary>
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<ScheduleRepository> _logger;

        public ScheduleRepository(IAttendanceControlDBContext dbContext,
                                  ILogger<ScheduleRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        ///     Recupera la lista de todos los horarios posibles
        /// </summary>
        /// <returns>
        ///     Retorna la lista de horarios
        /// </returns>
        public async Task<List<ScheduleEntity>> GetByShift(int shiftId)
        {
            List<ScheduleEntity> scheduleEntities =  await _dbContext.ScheduleEntities.
                Where(s=>s.ShiftId == shiftId).ToListAsync();

            _logger.LogInformation("Lista de horarios recuperada de la base de datos.");

            return scheduleEntities;
        }

    }
}
