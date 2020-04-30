using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.DTOs;
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

        // POST api/admins
        /// <summary>
        ///     Ruta de la petición de login del administrador
        /// </summary>
        /// <param name="admin">
        ///     Objecto Admin que contiene las credenciales del administrador
        /// </param>
        /// <returns>
        ///     Retorna un objeto SignInResponse o retorna un error 404 
        ///     y un mensaje en caso de que las credenciales sean erronéas
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] Admin admin)
        {
            _logger.LogInformation("Petición de login del administrador recibida");

            try
            {
                AdminSignInResponse result = await _adminService.SignIn(admin);

                _logger.LogInformation("Token de administrador retornado");

                return Ok(result);
            }
            catch(WrongCredentialsException ex)
            {
                _logger.LogWarning("Credenciales de administrador incorrectas");

                return NotFound(ex.Message);
            }         
        }

    }
}