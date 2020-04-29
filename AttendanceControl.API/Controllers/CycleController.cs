using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.Business.Models;
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

        public CycleController(ICycleService cycleService, ILogger<CycleController> logger)
        {
            _cycleService = cycleService;
            _logger = logger;
        }

        // POST api/cycles
        /// <summary>
        ///     Ruta de la petición para crear un nuevo ciclo formativo
        /// </summary>
        /// <param name="cycle"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Cycle cycle)
        {
            _logger.LogInformation("Petición de creación de ciclo formativo recibida");

            try
            {
                var result = await _cycleService.Save(cycle);

                _logger.LogInformation("Ciclo formativo creado retornado");

                return Ok(result);
            }
            catch (GradeNameDuplicateEntryException ex)
            {
                _logger.LogWarning(ex.Message);

                return Conflict(ex.Message);
            }
        }

        // PUT api/cycles/{id}
        [HttpPut("{cycleId}")]
        public async Task<IActionResult> UpdateName(int cycleId,[FromBody] string name)
        {

            _logger.LogInformation("Petición de creación de ciclo recibida");

            try
            {
                var result = await _cycleService.UpdateName(cycleId,name);

                _logger.LogInformation("Petición de creación de ciclo exitosa");

                return Ok(result);
            }
            catch (GradeNameDuplicateEntryException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // GET api/cycles
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {

            _logger.LogInformation("Petición de listado de grados recibida");

            var result = await _cycleService.GetAll();

            _logger.LogInformation("AAAAAAAAAAAAAAAAAAAA" + result[0].Shift);
            return Ok(result);

        }

        // DELETE api/cycless/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            _logger.LogInformation("Petición para borrar un grado recibida");

            var result = await _cycleService.Delete(id);

            return Ok(result);

        }
    }
}