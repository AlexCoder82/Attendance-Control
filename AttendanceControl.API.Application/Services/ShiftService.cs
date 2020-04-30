using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.Mappers;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Services
{
    /// <summary>
    ///     Lógica relaciona a los turnos de los ciclos formativos
    /// </summary>
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly ILogger<ShiftService> _logger;

        public ShiftService(IShiftRepository shiftRepository,
                            ILogger<ShiftService> logger)
        {
            _shiftRepository = shiftRepository;
            _logger = logger;
        }

        /// <summary>
        ///     Lista todos los turnos disponibles
        /// </summary>
        /// <returns>
        ///     Retorna una lista de objetos Shift
        /// </returns>
        public async Task<List<Shift>> GetAll()
        {

            List<ShiftEntity> shiftEntities = await _shiftRepository.GetAll();

            List<Shift> shifts = shiftEntities.Select(s => ShiftMapper.Map(s)).ToList();

            return shifts;

        }
    }
}
