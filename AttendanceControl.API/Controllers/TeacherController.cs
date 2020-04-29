using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.Errors;
using System;
using Microsoft.AspNetCore.Http;

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

        // POST api/teachers/sign-up?dni=dni
        [HttpPost("sign-up/{dni}")]
        public async Task<IActionResult> Register( string dni,[FromBody] TeacherCredentials teacherCredentials)
        {

            _logger.LogInformation("Petición de registro de credenciales de un profesor con dni " + dni);

            try
            {
                var result = await _teacherService.Register(dni, teacherCredentials);

                return Ok(result);
            }
            catch(Exception ex )
            {
                if (ex is WrongDniException || ex is TeacherAlreadyRegistered)
                {
                    ApiError error = new ApiError()
                    {
                        Timestamp = DateTime.Now,
                        StatusCode = StatusCodes.Status409Conflict,
                        Error = ex.GetType().Name,
                        Message = ex.Message
                    };

                    _logger.LogWarning(error.ToString() + dni);

                    return Conflict(error);
                }
                else
                {
                    throw ex;
                }
                            
            }
           
            

        }

        // POST api/teachers/sign-in?dni=dni
        [HttpPost("sign-in/{dni}")]
        public async Task<IActionResult> SignIn( string dni)
        {
            _logger.LogInformation("Petición de conexion del profesor con dni " + dni);

            try
            {
                var result = await _teacherService.SignIn(dni);

                return Ok(result);
            }catch(WrongDniException ex)
            {
                ApiError error = new ApiError()
                {
                    Timestamp = DateTime.Now,
                    StatusCode = StatusCodes.Status404NotFound,
                    Error = ex.GetType().Name,
                    Message = ex.Message
                };

                _logger.LogWarning(error.ToString() + dni);

                return NotFound(error);
            }
          
           

           

            

        }


    }
}