using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService,
                                 ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }


        // GET api/students/{page}?lastname=lastname
        /// <summary>
        ///     Ruta para conseguir la lista de alumnos, el curso y las asignaturas
        ///     que cursan ,filtrada por apellido y por  página.
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="page">
        ///     El número de página 
        /// </param>
        /// <param name="lastname">
        ///     El apellido buscado
        /// </param>
        /// <returns>
        ///     Retorna una lista de objetos Student
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpGet("{page}")]
        public async Task<IActionResult> GetByPageLikeLastNameIncludingCourseAndSubjects(int page, [FromQuery] string lastname)
        {

            _logger.LogInformation("Peticion de listado de alumnos cuyo apellido empieza por " + lastname +
                        " de la página " + page);

            List<Student> result = await _studentService.GetByPageLikeLastNameIncludingCourseAndSubjects(lastname, page);

            _logger.LogInformation("Listado de alumnos retornado");
            return Ok(result);
        }


        // POST api/students
        /// <summary>
        ///     Ruta de la peticion para crar un nuevo alumno.
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="student">
        ///     El objeto Student que contiene los datos del alumno
        /// </param>
        /// <returns>
        ///     Retorna el objeto Student creado
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Student student)
        {

            _logger.LogInformation("Peticion para crear un nuevo alumno recibidad");

            Student result = await _studentService.Save(student);

            _logger.LogInformation("Alumno creado retornado");

            return Ok(result);

        }

        // PUT api/students
        /// <summary>
        ///     Ruta de la peticion para actualizar los datos de un alumno
        /// </summary>
        /// <param name="student">
        ///     El objeto Student con los nuevos datos del alumno
        /// </param>
        /// <returns>
        ///     Retorna el objeto Student actualizado
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Student student)
        {

            _logger.LogInformation("Peticion para actualizar el alumno con id " + student.Id);

            Student result = await _studentService.Update(student);

            _logger.LogInformation("Alumno actualizado retornado");

            return Ok(result);

        }

        // PUT api/students/{studentId}/courses/{courseId}
        /// <summary>
        ///     Ruta de la peticion para asignar un nuevo curso a un alumno.
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno
        /// </param>
        /// <param name="courseId">
        ///     El id del curso
        /// </param>
        /// <returns>
        ///     Retorna un objeto Student con el curso actualizado
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("{studentId}/courses/{courseId}")]
        public async Task<IActionResult> UpdateCourse(int studentId, int courseId)
        {

            _logger.LogInformation("Peticion para asignar el curso " +
                        "con id " + courseId + " al alumno con id " + studentId);

            Student result = await _studentService.UpdateCourse(studentId, courseId);

            _logger.LogInformation("Alumno actualizado retornado");

            return Ok(result);

        }

        // PUT api/students/{id}/courses
        /// <summary>
        ///     Ruta para retirar la asignacion de curso a un alumno.
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno
        /// </param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("{studentId}/courses")]
        public async Task<IActionResult> RemoveCourse(int studentId)
        {

            _logger.LogInformation("Peticion para retirar la asignacion " +
                            "de curso al alumno con id " + studentId);

            bool result = await _studentService.RemoveCourse(studentId);

            _logger.LogInformation("Alumno modificado con éxito");

            return Ok(result);

        }

        // PUT api/students/{id}/subjects?subjectIds = ?
        /// <summary>
        ///     Ruta de la peticion para asignar nuevas asignaturas a un alumno.
        ///     Reservada al role Admin
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno
        /// </param>
        /// <param name="subjectIds">
        ///     El array de ids de las asignaturas
        /// </param>
        /// <returns>
        ///     Retorna el objeto Student con los datos actualizados
        /// </returns>
        [Authorize(Roles = Role.ADMIN)]
        [HttpPut("{studentId}/subjects")]
        public async Task<IActionResult> UpdateSubjects(int studentId, [FromQuery] int[] subjectIds)
        {

            _logger.LogInformation("Peticion para asignar nuevas asignaturas al " +
                        "alumno con id " + studentId + " recibida");

            Student result = await _studentService.UpdateSubjects(studentId, subjectIds);

            _logger.LogInformation("Alumno actualizado retornado");

            return Ok(result);

        }

    }
}
