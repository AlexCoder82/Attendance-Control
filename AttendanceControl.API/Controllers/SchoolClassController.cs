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
    [Route("api/schoolclasses")]
    [ApiController]
    public class SchoolClassController : ControllerBase
    {
        private readonly ISchoolClassService _schoolClassService;
        private readonly ILogger<SchoolClassController> _logger;

        public SchoolClassController(ISchoolClassService schoolClassService,
                                     ILogger<SchoolClassController> logger)
        {
            _schoolClassService = schoolClassService;
            _logger = logger;
        }



        // PUT api/schoolclasses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> SetNotCurrent(int id)
        {

            //  _logger.LogInformation("Petición para borrar un grado recibida");

            var result = await _schoolClassService.Cancel(id);

            return Ok(result);

        }

        // POST api/schoolclasses
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SchoolClass schoolClass)
        {

            //  _logger.LogInformation("Petición para borrar un grado recibida");

            var result = await _schoolClassService.Save(schoolClass);

            return Ok(result);

        }

        // GET api/schoolclasses/courses/id
        [HttpGet("courses/{courseId}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {

            _logger.LogInformation("Petición de listado de clases de un curso recibida");


            var result = await _schoolClassService.GetByCourse(courseId);

            _logger.LogInformation("Listado de clases de un curso exitosa devuelto");

            return Ok(result);

        }

        // GET api/schoolclasses/teachers/teacherId
        [HttpGet("teachers/{teacherId}")]
        public async Task<IActionResult> GetByTeacher(int teacherId)
        {

            _logger.LogInformation("Petición de listado de alumnos por un profesor recibida");


            var result = await _schoolClassService.GetByTeacher(teacherId);

            _logger.LogInformation("Listado de alumnos por un profesor retornada");

            return Ok(result);

        }
    }
}