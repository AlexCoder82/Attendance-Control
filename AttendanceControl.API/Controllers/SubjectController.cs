using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly ILogger<SubjectController> _logger;

        public SubjectController(ISubjectService subjectService,
                                 ILogger<SubjectController> logger)
        {
            _subjectService = subjectService;
            _logger = logger;
        }

        // POST api/subjects
        /// <summary>
        ///     Ruta de la peticion para guardar una nueva asignatura
        /// </summary>
        /// <param name="subject">
        ///     El objeto Subject que contiene los datos de la asignatura
        /// </param>
        /// <returns>
        ///     Retorna el objeto Subject guardado o un error 409 
        ///     si se intenta crear una asignatura cuyo nombre ya existe
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Subject subject)
        {

            _logger.LogInformation("Petición para crear nueva asignatura recibida");

            try
            {
                var result = await _subjectService.Save(subject);

                _logger.LogInformation("Asignatura creada retornada");

                return Ok(result);
            }
            catch (SubjectNameDuplicateEntryException ex)
            {
                ApiError error = new ApiError()
                {
                    Timestamp = DateTime.Now,
                    StatusCode = StatusCodes.Status409Conflict,
                    Error = ex.GetType().Name,
                    Message = ex.Message
                };

                _logger.LogWarning(error.ToString());

                return Conflict(error);
            }

        }

        // PUT api/subjects
        /// <summary>
        ///     Ruta de la peticion para actualizar una asignatura.
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="subject">
        ///     El objeto Subject que contiene los nuevos datos de la asignatura
        /// </param>
        /// <returns>
        ///     Retorna el objeto Subject actualizado o un error 409 si
        ///     la asignatura tiene un nombre que ya existe
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Subject subject)
        {
            _logger.LogInformation("Petición para modificar la asignatura" +
                " con id " + subject.Id + " recibida");

            try
            {
                Subject result = await _subjectService.Update(subject);

                _logger.LogInformation("Asignatura actualizada retornada");

                return Ok(result);
            }
            catch (SubjectNameDuplicateEntryException ex)
            {
                ApiError error = new ApiError()
                {
                    Timestamp = DateTime.Now,
                    StatusCode = StatusCodes.Status409Conflict,
                    Error = ex.GetType().Name,
                    Message = ex.Message
                };

                _logger.LogWarning(error.ToString());

                return Conflict(error);
            }

        }

        // PUT api/subjects/{id}/teachers/{id}
        /// <summary>
        ///     Peticion para cambiar el profesor asignado a una asignatura.
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="subjectId">
        ///     El id de la asignatura
        /// </param>
        /// <param name="teacherId">
        ///     El id del profesor
        /// </param>
        /// <returns>
        ///     Retorna un objeto Subject con los datos actualizados
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("{subjectId}/teachers/{teacherId}")]
        public async Task<IActionResult> UpdateAssignedTeacher(int subjectId, int teacherId)
        {
            _logger.LogInformation("Petición para asignar el profesor " +
                    "con id " + teacherId + " a la asignatura con id " + subjectId);

            Subject result = await _subjectService.UpdateAssignedTeacher(subjectId, teacherId);

            _logger.LogInformation("Asignatura actualizada retornada");

            return Ok(result);

        }

        // PUT api/subjects/{id}/teachers/{id}
        /// <summary>
        ///     Ruta de la peticion para retirar la asignacion de profesor a una asignatura
        /// </summary>
        /// <param name="subjectId">
        ///     El id de la asignatura
        /// </param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("{subjectId}/teachers")]
        public async Task<IActionResult> RemoveAssignedTeacher(int subjectId)
        {
            _logger.LogInformation("Petición para retirar la asignación " +
                "de profesor a la asignatura con id " + subjectId);

            bool result = await _subjectService.RemoveAssignedTeacher(subjectId);

            _logger.LogInformation("Asignatura actualizada retornada");

            return Ok(result);

        }

        // GET api/subjects
        /// <summary>
        ///     Ruta de la peticion para listar todas las asignaturas
        ///     y los profesores asignados.
        ///     Reservada al role Admin
        /// </summary>
        /// <returns>
        ///     Retorna una lista de objetos Subject
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpGet]
        public async Task<IActionResult> GetAllIncludingAssignedTeacher()
        {

            _logger.LogInformation("Petición de listado de asignaturas recibida");

            List<Subject> result = await _subjectService.GetAllIncludingAssignedTeacher();

            _logger.LogInformation("Listado de asignatura retornados");

            return Ok(result);

        }

        // GET api/subjects/courses/{courseId}
        /// <summary>
        ///     Ruta de la peticion para listar todas las asignaturas
        ///     de un curso.
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="courseId">
        ///     El id del curso
        /// </param>
        /// <returns>
        ///     Retorna una lista de objetos Subject
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("courses/{courseId}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {

            _logger.LogInformation("Petición de listado de asignaturas " +
                "del curso con id " + courseId + " recibida");

            List<Subject> result = await _subjectService.GetByCourse(courseId);

            _logger.LogInformation("Listado de asignaturas retornado");

            return Ok(result);

        }

    }
}