using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceControl.API.Controllers
{
    [Route("api/schoolclasses")]
    [ApiController]
    public class SchoolClassController : ControllerBase
    {
        private readonly ISchoolClassService _schoolClassService;

        public SchoolClassController(ISchoolClassService schoolClassService)
        {
            _schoolClassService = schoolClassService;
        }


        // PUT api/schoolclasses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

          //  _logger.LogInformation("Petición para borrar un grado recibida");

            var result = await _schoolClassService.SetNotCurrent(id);

            return Ok(result);

        }

        // POST api/schoolclasses
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SchoolClass schoolClass)
        {

            //  _logger.LogInformation("Petición para borrar un grado recibida");

            var result = await _schoolClassService.Save(schoolClass);

            return Ok(result);

        }
    }
}