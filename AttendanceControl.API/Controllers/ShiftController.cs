using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Controllers
{
    [Route("api/shifts")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;
        private readonly ILogger<ShiftController> _logger;

        public ShiftController(IShiftService shiftService, ILogger<ShiftController> logger)
        {
            _shiftService = shiftService;
            _logger = logger;
        }

        


        // GET api/shifts
        [HttpGet]
        public async Task<IActionResult> GetALL()
        {

            _logger.LogInformation("Petición de listado de turnos recibida");

            var result = await _shiftService.GetAll();

            return Ok(result);

        }

    }
}