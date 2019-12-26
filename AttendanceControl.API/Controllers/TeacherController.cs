using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Controllers
{
    [Route("api/teachers")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ITeacherService teacherService, ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        // GET api/teachers
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {

            _logger.LogInformation("Petición de listado deprofesores recibida");

            var result = await _teacherService.GetAll();

            return Ok(result);

        }

        // POST api/teachers
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Teacher teacher)
        {

            _logger.LogInformation("Petición de alta de profesor recibida");

            var result = await _teacherService.Save(teacher);

            return Ok(result);

        }

        // PUT api/teachers
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Teacher teacher)
        {

            _logger.LogInformation("Petición de modificación de un profesor recibida");

            var result = await _teacherService.Update(teacher);

            return Ok(result);

        }
    }
}