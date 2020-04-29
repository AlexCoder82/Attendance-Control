using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.Business.Models;
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

        // PUT api/courses
        /// <summary>
        ///     Ruta de la petición para asignar una
        ///     asignatura a un curso
        /// </summary>
        /// <param name="course"></param>
        /// <returns>
        ///     Retorna true o retorna un 404 con un mensaje
        ///     si por error se asigna dos veces la misma asignatura al curso
        /// </returns>
        [HttpPut("{courseId}/subjects/{subjectId}")]
        public async Task<IActionResult> AssignSubject(int courseId, int subjectId)
        {
            _logger.LogInformation("Petición de actualización de las asignaturas de un curso");

            try
            {
                var result = await _courseService.AssignSubject(courseId,subjectId);

                _logger.LogInformation("Curso actualizado retornado");

                return Ok(result);
            }
            catch (CourseSubjectDuplicateEntryException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/courses
        /// <summary>
        ///     Ruta de la petición para asignar una
        ///     asignatura a un curso
        /// </summary>
        /// <param name="course"></param>
        /// <returns>
        ///     Retorna true o retorna un 404 con un mensaje
        ///     si por error se asigna dos veces la misma asignatura al curso
        /// </returns>
        [HttpDelete("{courseId}/subjects/{subjectId}")]
        public async Task<IActionResult> RemoveAssignedSubject(int courseId, int subjectId)
        {
            _logger.LogInformation("Petición de actualización de las asignaturas de un curso");

            try
            {
                var result = await _courseService.RemoveAssignedSubject(courseId, subjectId);

                _logger.LogInformation("Curso actualizado retornado");

                return Ok(result);
            }
            catch (CourseSubjectDuplicateEntryException ex)
            {
                return NotFound(ex.Message);
            }
        }


        // Get api/courses
        /// <summary>
        ///     Ruta de la petición de  la lista de todos los cursos
        /// </summary>
        /// <returns>
        ///     La lista de todos los cursos
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {

            _logger.LogInformation("Petición de listado de todos los cursos recibida");


            var result = await _courseService.GetAll();

            _logger.LogInformation("Listado de todos los cursos retornado");

            return Ok(result);

        }

    }
}