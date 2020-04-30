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
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CycleController> _logger;

        public CourseController(ICourseService courseService,
                                ILogger<CycleController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        // PUT api/courses/{courseId}/subjects/{subjectId}
        /// <summary>
        ///     Ruta de la petición para añadir una
        ///     asignatura a un curso.
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="courseId">
        ///     El id del curso.
        /// </param>
        /// <param name="subjectId">
        ///     El id de la asignatura
        /// </param>
        /// <returns>
        ///     Retorna true o retorna un 409 con un mensaje
        ///     si por error se intenta añadir al curso una asignatura que ya tiene
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("{courseId}/subjects/{subjectId}")]
        public async Task<IActionResult> AssignSubject(int courseId, int subjectId)
        {

            _logger.LogInformation(string.Format("Petición para añadir la asignatura " +
                "con id {0} al curso con id {1}", subjectId, courseId));

            try
            {
                bool result = await _courseService.AssignSubject(courseId, subjectId);

                _logger.LogInformation("Asignatura añadida con éxito");

                return Ok(result);
            }
            catch (CourseSubjectDuplicateEntryException ex)
            {
                _logger.LogWarning("Error: El curso ya tiene la asignatura.");

                return Conflict(ex.Message);
            }

        }

        // DELETE api/courses/{courseId}/subjects/{subjectId}
        /// <summary>
        ///     Ruta de la petición para retirar una
        ///     asignatura de un curso
        /// </summary>
        /// <param name="courseId">
        ///     El id del curso.
        /// </param>
        /// <param name="subjectId">
        ///     El id de la asignatura
        /// </param>
        /// <returns>
        ///     Retorna true 
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpDelete("{courseId}/subjects/{subjectId}")]
        public async Task<IActionResult> RemoveAssignedSubject(int courseId, int subjectId)
        {

            _logger.LogInformation(string.Format("Petición para retirar la asignatura " +
                "con id {0} del curso con id {1}", subjectId, courseId));

            bool result = await _courseService.RemoveAssignedSubject(courseId, subjectId);

            _logger.LogInformation("Asignatura retirada del curso con éxito");

            return Ok(result);

        }


        // Get api/courses
        /// <summary>
        ///     Ruta de la petición de la lista de todos los cursos
        /// </summary>
        /// <returns>
        ///     Retorna la lista de todos los cursos
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {

            _logger.LogInformation("Petición de listado de todos los cursos recibida");

            List<Course> result = await _courseService.GetAll();

            _logger.LogInformation("Listado de todos los cursos retornado");

            return Ok(result);

        }

    }
}