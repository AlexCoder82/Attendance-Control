using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IAuth;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }

        // POST api/admin
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] Admin admin)
        {

            _logger.LogInformation("Petición de login " +
                "de administrador recibida");

            try
            {
                var result = await _adminService.SignIn(admin);

                _logger.LogInformation("Petición de login " +
                "de administrador exitosa");

                return Ok(result);
            }
            catch(WrongCredentialsException ex)
            {
                _logger.LogWarning("Credenciales de administrador incorrectos");

                return NotFound(ex.Message);
            }

            
        }
    }
}