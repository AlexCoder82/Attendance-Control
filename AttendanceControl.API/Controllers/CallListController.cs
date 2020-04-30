using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.DTOs;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Controllers
{
    [Route("api/call-list")]
    [ApiController]
    public class CallListController : ControllerBase
    {
        private readonly ICallListService _callListService;
        private readonly ILogger<CallListController> _logger;

        public CallListController(ICallListService callListService,
                                     ILogger<CallListController> logger)
        {
            _callListService = callListService;
            _logger = logger;
        }

        // GET api/callList
        /// <summary>
        ///     Ruta de la petición del listado de alumnos de una clase.
        ///     Reservador al role Profesor
        /// </summary>
        /// <param name="schoolClassIds">
        ///     Array con los ids de las clases 
        /// </param>
        /// <returns>
        ///     Retorna una lista de objetos SchoolClassStudent
        /// </returns>
        [Authorize(Roles = Role.TEACHER)]
        [HttpGet]
        public async Task<IActionResult> GetCallList([FromQuery]int[] schoolClassIds)
        {

            _logger.LogInformation("Petición de listado de alumnos por un profesor recibida");

            List<SchoolClassStudent> result = await _callListService.Get(schoolClassIds);

            _logger.LogInformation("Listado de alumnos retornado");

            return Ok(result);

        }

        // POST api/callList
        /// <summary>
        ///     Ruta de la petición de envio de la lista de alumnos.
        ///     Reservada al role Profesor
        /// </summary>
        /// <param name="callList">
        ///     Lista de objetos SchoolClassStudent
        /// </param>
        /// <returns>
        ///     Retorna true 
        /// </returns>
        [HttpPost()]
        [Authorize(Roles = Role.TEACHER)]
        public async Task<IActionResult> PostCallList([FromBody]List<SchoolClassStudent> callList)
        {

            _logger.LogInformation("Petición de envio de listado de alumnos");
            
            bool result = await _callListService.Post(callList);

            _logger.LogInformation("Envío realizado con éxito");

            return Ok(result);

        }

    }
}