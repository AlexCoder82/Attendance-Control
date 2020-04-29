using System;
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Models;
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

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        //// GET api/students?dni=dni
        //[HttpGet]
        //public async Task<IActionResult> GetLikeDniIncludingCourseAndSubjects([FromQuery] string dni,[FromQuery] string lastname)
        //{
        //    var result = await _studentService.GetLikeDniIncludingCourseAndSubjects(dni);

        //    return Ok(result);
        //}

        // GET api/students?lastname=lastname
        [HttpGet("{page}")]
        public async Task<IActionResult> GetByPageLikeLastNameIncludingCourseAndSubjects(int page,[FromQuery] string lastname)
        {
            var result = await _studentService.GetByPageLikeLastNameIncludingCourseAndSubjects(lastname,page);

            return Ok(result);
        }


        // GET api/students/courses/{courseId}
        [HttpGet("courses/{courseId}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            var result = await _studentService.GetByCourse(courseId);

            return Ok(result);
        }

        // POST api/students
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Student student)
        {
            var result = await _studentService.Save(student);

            return Ok(result);
        }

        // PUT api/students
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Student student)
        {
            var result = await _studentService.Update(student);

            return Ok(result);
        }

        // PUT api/students/{id}/courses/{id}
        [HttpPut("{studentId}/courses/{courseId}")]
        public async Task<IActionResult> UpdateCourse(int studentId,int courseId)
        {
            var result = await _studentService.UpdateCourse(studentId,courseId);

            return Ok(result);
        }

        // PUT api/students/{id}/courses
        [HttpPut("{studentId}/courses")]
        public async Task<IActionResult> RemoveCourse(int studentId)
        {
            var result = await _studentService.RemoveCourse(studentId);

            return Ok(result);
        }

        // PUT api/students/{id}/subjects/{subjectsIds}
        [HttpPut("{studentId}/subjects")]
        public async Task<IActionResult> UpdateSubjects(int studentId,[FromQuery] int[] subjectIds)
        {
            var result = await _studentService.UpdateSubjects(studentId, subjectIds);

            return Ok(result);
        }
    }
}
