using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.DTOs;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Controllers
{
    [Route("api/call-list")]
    [ApiController]
    public class CallListController : ControllerBase
    {
        private readonly ICallListService _callListService;
        private readonly ILogger<CallListController> _logger;

        public CallListController(ICallListService callListService,
                                     ILogger<CallListController> logger)
        {
            _callListService = callListService;
            _logger = logger;
        }

        // GET api/callList
        [Authorize(Roles = Role.TEACHER)]

        public async Task<IActionResult> GetCallList([FromQuery]int[] schoolClassIds)
        {

            _logger.LogInformation("Petición de listado de alumnos de una clase por un profesor");


            var result = await _callListService.Get(schoolClassIds);

            _logger.LogInformation("Listado de alumnos retornado");


            return Ok(result);

        }
        // POST api/callList
        [HttpPost()]
        public async Task<IActionResult> PostCallList([FromBody]List<SchoolClassStudent> callList)
        {

            _logger.LogInformation("Petición de envio de listado de alumnos");
            

            var result = await _callListService.Post(callList);

            _logger.LogInformation("Listado de alumnos retornado");


            return Ok(result);

        }

    }
}