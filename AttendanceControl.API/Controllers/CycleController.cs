using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Controllers
{
    [Route("api/cycles")]
    [ApiController]
    public class CycleController : ControllerBase
    {
        private readonly ICycleService _cycleService;
        private readonly ILogger<CycleController> _logger;

        public CycleController(ICycleService cycleService,
                               ILogger<CycleController> logger)
        {
            _cycleService = cycleService;
            _logger = logger;
        }

        // POST api/cycles
        /// <summary>
        ///     Ruta de la petición para crear un nuevo ciclo formativo
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="cycle">
        ///     El objecto Cycle 
        /// </param>
        /// <returns>
        ///     Retorna el ciclo creado o retorna un error 409 si se 
        ///     intenta crear un ciclo con un nombre que ya tiene otro ciclo
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Cycle cycle)
        {

            _logger.LogInformation("Petición de creación de ciclo formativo recibida");

            try
            {
                Cycle result = await _cycleService.Save(cycle);

                _logger.LogInformation("Ciclo formativo creado retornado");

                return Ok(result);
            }
            catch (GradeNameDuplicateEntryException ex)
            {
                _logger.LogWarning(ex.Message);

                return Conflict(ex.Message);
            }

        }

        // PUT api/cycles
        /// <summary>
        ///     Ruta de la petición de modificación de un ciclo
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="cycle">
        ///     El objeto Cycle con los nuevos datos del ciclo
        /// </param>
        /// <returns>
        ///     Retorna true o un 409 si el ciclo tiene un nombre que ya 
        ///     tiene otro ciclo
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Cycle cycle)
        {

            _logger.LogInformation("Petición de actualziacion del ciclo" +
                " con id " + cycle.Id + " recibida");

            try
            {
                bool result = await _cycleService.Update(cycle);

                _logger.LogInformation("Petición de actualización de un ciclo exitosa");

                return Ok(result);
            }
            catch (GradeNameDuplicateEntryException ex)
            {
                _logger.LogWarning("Error: el nombre del ciclo ya existe");

                return Conflict(ex.Message);
            }

        }

        // GET api/cycles
        /// <summary>
        ///     Ruta de la petición del listado de todos los 
        ///     ciclos formativos
        ///     Reservada al role Admin
        /// </summary>
        /// <returns>
        ///     Retorna la lista de objetos Cycle
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {

            _logger.LogInformation("Petición de listado de todos los ciclos formativos recibida");

            List<Cycle> result = await _cycleService.GetAll();

            _logger.LogInformation("Lista de ciclos formativos retornada");

            return Ok(result);

        }

    }
}