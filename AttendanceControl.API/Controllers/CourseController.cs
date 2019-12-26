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
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CycleController> _logger;

        public CourseController(ICourseService courseService, ILogger<CycleController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        // PUT api/courses
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Course course)
        {

            _logger.LogInformation("Petición de actualización de curso");

            try
            {
                var result = await _courseService.UpdateCourse(course);

                _logger.LogInformation("Petición de actualización de curso exitosa");

                return Ok(result);
            }
            catch (CourseSubjectDuplicateEntryException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET api/courses/{id}/schoolclasses
        [HttpGet("{courseId}/schoolclasses")]
        public async Task<IActionResult> GetSchoolClasses(int courseId)
        {

            _logger.LogInformation("Petición de listado de clases de un curso recibida");

            try
            {
                var result = await _courseService.GetSchoolClasses(courseId);

                _logger.LogInformation("Petición de listado de clases de un curso exitosa");

                return Ok(result);
            }
            catch (CourseSubjectDuplicateEntryException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}