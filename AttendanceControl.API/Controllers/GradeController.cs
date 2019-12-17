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
    [Route("api/grades")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly ICycleService _gradeService;
        private readonly ILogger<GradeController> _logger;

        public GradeController(ICycleService gradeService, ILogger<GradeController> logger)
        {
            _gradeService = gradeService;
            _logger = logger;
        }

        // POST api/grades
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Cycle grade)
        {

            _logger.LogInformation("Petición de creación de grado recibida");

            try
            {
                var result = await _gradeService.Save(grade);

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

            var result = await _gradeService.GetAll();

            return Ok(result);

        }
    }
}