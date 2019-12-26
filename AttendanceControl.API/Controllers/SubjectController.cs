using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.Errors;
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

        public SubjectController(ISubjectService subjectService, ILogger<SubjectController> logger)
        {
            _subjectService = subjectService;
            _logger = logger;
        }

        // POST api/subjects
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Subject subject)
        {
            _logger.LogInformation("Petición para crear nueva asignatura recibida");

            try
            {
                var result = await _subjectService.Save(subject);

                _logger.LogInformation("Respuesta de la petición para crear nueva asignatura enviada");

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
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Subject subject)
        {
            _logger.LogInformation("Petición para modificar una asignatura recibida");

            try
            {
                var result = await _subjectService.Update(subject);

                _logger.LogInformation("Respuesta de la petición para modificar una asignatura enviada");

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
        [HttpPut("{subjectId}/teachers/{teacherId}")]
        public async Task<IActionResult> UpdateAssignedTeacher(int subjectId, int teacherId)
        {
            _logger.LogInformation("Petición para asignar un profesor a una asignatura");


            var result = await _subjectService.UpdateAssignedTeacher(subjectId,teacherId);

            return Ok(result);

        }

        // DELETE api/subjects/{id}/teachers/{id}
        [HttpPut("{subjectId}/teachers")]
        public async Task<IActionResult> RemoveAssignedTeacher(int subjectId)
        {
            _logger.LogInformation("Petición para retirar la asignación de un profesor a una asignatura");


            var result = await _subjectService.UpdateAssignedTeacher(subjectId,null);

            return Ok(result);

        }

        // GET api/subjects
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {

            _logger.LogInformation("Petición de listado de asignaturas recibida");

            var result = await _subjectService.GetAll();

            return Ok(result);

        }

    }
}