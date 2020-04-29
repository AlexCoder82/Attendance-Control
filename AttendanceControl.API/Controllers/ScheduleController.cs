﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Controllers
{
    [Route("api/schedules")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly ILogger<ScheduleController> _logger;

        public ScheduleController(IScheduleService scheduleService, ILogger<ScheduleController> logger)
        {
            _scheduleService = scheduleService;
            _logger = logger;
        }

        // GET api/schedules/shifts/{shiftId}
        [HttpGet("/shifts/{shiftId}")]
        public async Task<IActionResult> GetByShift(int shiftId)
        {

            _logger.LogInformation("Petición de listado de horarios");

            var result = await _scheduleService.GetByShift(shiftId);

            return Ok(result);

        }
    }
}