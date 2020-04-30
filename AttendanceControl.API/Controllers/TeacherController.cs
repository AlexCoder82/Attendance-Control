using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.Errors;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Application.Contracts.DTOs;

namespace AttendanceControl.API.Controllers
{
    [Route("api/teachers")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ITeacherService teacherService,
                                 ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        // GET api/teachers
        /// <summary>
        ///     Ruta de la peticion para listar todas las profesores.
        ///     Reservada al role Admin
        /// </summary>
        /// <returns>
        ///     Retorna una lista de objetos Teacher
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {

            _logger.LogInformation("Petición de listado de profesores recibida");

            var result = await _teacherService.GetAll();

            _logger.LogInformation("Listado de profesores retornado");

            return Ok(result);

        }

        // POST api/teachers
        /// <summary>
        ///     Ruta de la peticion para crear un nuevo profesor.
        ///     Reservada al role Admin.
        /// </summary>
        /// <param name="teacher">
        ///     El objeto Teacher que contiene los datos del profesor
        /// </param>
        /// <returns>
        ///     Retorna el objeto Teacher con los datos del profesor creado
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Teacher teacher)
        {

            _logger.LogInformation("Petición de alta de profesor recibida");

            Teacher result = await _teacherService.Save(teacher);

            _logger.LogInformation("Profesor creado retornado");

            return Ok(result);

        }

        // PUT api/teachers
        /// <summary>
        ///     Ruta de la peticion para actualizar los datos 
        ///     de un profesor.
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="teacher">
        ///     El objeto Teacher que contiene los nuevos datos del profesor
        /// </param>
        /// <returns>
        ///     Retorna el objeto Teacher actualizado o un error 409 si el dni 
        ///     del profesor ya lo tiene otro profesor
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Teacher teacher)
        {

            _logger.LogInformation("Petición de modificación de un profesor recibida");

            try
            {
                Teacher result = await _teacherService.Update(teacher);

                _logger.LogInformation("Profesor actualizado retornado");

                return Ok(result);

            }
            catch (DniDuplicateEntryException ex)
            {
                _logger.LogWarning(ex.Message);

                return Conflict(ex.Message);
            }

        }


        // POST api/teachers/sign-in?dni=dni
        /// <summary>
        ///     Ruta de la peticion de login de un profesor
        /// </summary>
        /// <param name="dni">
        ///     El dni del profesor 
        /// </param>
        /// <returns>
        ///     Retorna un objeto TeacherSignInResponse o un error 404 si su dni
        ///     no es reconocido
        /// </returns>
        [HttpPost("sign-in/{dni}")]
        public async Task<IActionResult> SignIn(string dni)
        {
            _logger.LogInformation("Petición de conexion del profesor con dni " + dni);

            try
            {
                TeacherSignInResponse result = await _teacherService.SignIn(dni);

                _logger.LogInformation("Profesor logeado con éxito");

                return Ok(result);
            }
            catch (WrongDniException ex)
            {
                ApiError error = new ApiError()
                {
                    Timestamp = DateTime.Now,
                    StatusCode = StatusCodes.Status404NotFound,
                    Error = ex.GetType().Name,
                    Message = ex.Message
                };

                _logger.LogWarning(error.ToString());

                return NotFound(error);
            }

        }

    }
}