using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Controllers
{
    [Route("api/absences")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;
        private readonly ILogger<AbsenceController> _logger;

        public AbsenceController(IAbsenceService absenceService,
                                 ILogger<AbsenceController> logger)
        {
            _absenceService = absenceService;
            _logger = logger;
        }

        // Get api/absences/students/{studentId}
        /// <summary>
        ///     Ruta de la petición de listado de ausencias de un alumno
        ///     Reservada al administrador
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno
        /// </param>
        /// <returns>
        ///     Retorna la lista de ausencias del alumno
        /// </returns>
        [HttpGet("students/{studentId}")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            _logger.LogInformation("Petición de listado de todas las ausencias de un alumno recibida");

            var result = await _absenceService.GetByStudent(studentId);

            _logger.LogInformation("Listado de todas las ausencias de un alumno retornado al cliente");

            return Ok(result);
        }

        // PUT api/absences/{absenceId}
        /// <summary>
        ///     Ruta de la petición para establecer si una ausencia
        ///     esta justificada o no.
        ///     Ruta reservada al administrador
        /// </summary>
        /// <param name="absenceId">
        ///     El id de la ausencia 
        /// <param name="isExcused">
        ///     Booleano que indica si la ausencia esta justificada o no
        /// </param>
        /// <returns>
        ///     Retorna true indicando que el cambio se ha realizado correctamente
        /// </returns>
        [Authorize(Roles = Role.TEACHER)]
        [HttpPut("{absenceId}")]
        public async Task<IActionResult> SetExcused(int absenceId,
                                                    [FromBody] bool isExcused)
        {
            _logger.LogInformation("Petición para establecer la justificación " +
                "de la ausencia con id " + absenceId);

            var result = await _absenceService.SetExcused(absenceId, isExcused);

            _logger.LogInformation("Respuesta positiva a la petición para " +
                "establecer la justificación de la ausencia con id " + absenceId);

            return Ok(result);
        }
      
    }
}