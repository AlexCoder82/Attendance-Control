using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.Business.Models;
using Microsoft.AspNetCore.Http;
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

        // POST api/grades
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Cycle cycle)
        {

            _logger.LogInformation("Petición de creación de grado recibida");

            try
            {
                var result = await _cycleService.Save(cycle);

                _logger.LogInformation("Petición de creación de grado exitosa");

                return Ok(result);
            }
            catch (GradeNameDuplicateEntryException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT api/cycles
        [HttpPut]
        public async Task<IActionResult> UpdateName([FromBody] Cycle cycle)
        {

            _logger.LogInformation("Petición de creación de grado recibida");

            try
            {
                var result = await _cycleService.Update(cycle);

                _logger.LogInformation("Petición de creación de grado exitosa");

                return Ok(result);
            }
            catch (GradeNameDuplicateEntryException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET api/grades
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {

            _logger.LogInformation("Petición de listado de grados recibida");

            var result = await _cycleService.GetAll();

            return Ok(result);

        }

        // DELETE api/grades/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            _logger.LogInformation("Petición para borrar un grado recibida");

            var result = await _cycleService.Delete(id);

            return Ok(result);

        }
    }
}