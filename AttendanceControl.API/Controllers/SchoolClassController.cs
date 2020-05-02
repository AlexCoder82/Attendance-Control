using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.Business.Models;
using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        ///     Ruta para cancelar una clase.
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="id">
        ///     El id de la clase
        /// </param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("{id}")]
        public async Task<IActionResult> SetNotCurrent(int id)
        {

            _logger.LogInformation("Petición para cancelar la clase con id " + id);

            bool result = await _schoolClassService.Cancel(id);

            _logger.LogInformation("Clase cancelada con éxito");

            return Ok(result);

        }

        // POST api/schoolclasses
        /// <summary>
        ///     Ruta de la petición para crear una nueva clase
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="schoolClass">
        ///     El objeto SchoolClass que contiene los datos de la clase
        /// </param>
        /// <returns>
        ///     Retorna la clase creada
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SchoolClass schoolClass)
        {

            _logger.LogInformation("Petición para crear una clase");

            try
            {
                SchoolClass result = await _schoolClassService.Save(schoolClass);

                _logger.LogInformation("Clase creada con éxito");

                return Ok(result);
            }
            catch(TeacherAlreadyTeachingException ex)
            {
                _logger.LogWarning(ex.Message);

                return Conflict(ex.Message);
            }
           
        }

        // GET api/schoolclasses/courses/id
        /// <summary>
        ///     Ruta de la peticion de listado de todas las clases de un curso
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="courseId">
        ///     El id del curso
        /// </param>
        /// <returns>
        ///     La lista de objetos SchoolClass
        /// </returns>
        [HttpGet("courses/{courseId}")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<IActionResult> GetByCourse(int courseId)
        {

            _logger.LogInformation("Petición de listado de todas " +
                "las clases del curso con id " + courseId);


            List<SchoolClass> result = await _schoolClassService.GetByCourse(courseId);

            _logger.LogInformation("Listado de clases retornado");

            return Ok(result);

        }

    }
}