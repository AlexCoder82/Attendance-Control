using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.DTOs;
using AttendanceControl.API.Application.Contracts.IAuth;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.Mappers;
using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.Extensions.Logging;

namespace AttendanceControl.API.Application.Services
{    
    /// <summary>
    ///     Lógica relacionada a los admins
    /// </summary>
    public class AdminService:IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthService _authService;

        public AdminService(IAdminRepository adminRepository,
                            IAuthService authService,
                            ILogger<AdminService> logger)
        {
            _adminRepository = adminRepository;
            _authService = authService;
        }

        /// <summary>
        ///     Abre una sesión de administrador.
        ///     Envia al repositorio las credenciales del administrador.
        ///     Si el repositorio reconoce las credenciales, instancia 
        ///     un token para el role Admin.
        /// </summary>
        /// <param name="admin">
        ///     Objeto Admin con las credenciales
        /// </param>
        /// <exception cref="WrongCredentialsException">
        ///     Lanza WrongCredentialsException 
        /// </exception>
        /// <returns>
        ///     retorna un objeto que contiene el token creado, 
        ///     el id del administrador
        ///     y el role del administrador
        /// </returns>
        public async Task<AdminSignInResponse> SignIn(Admin admin)//Throw WrongCredentialsException
        {

            AdminEntity adminEntity = AdminMapper.Map(admin);

            var signedInAdmin = await _adminRepository.Exists(adminEntity);
          
            string token = _authService
                    .GenerateToken(signedInAdmin.AdminName + signedInAdmin.Password, Role.ADMIN);

            AdminSignInResponse response = new AdminSignInResponse
            {
                Id = adminEntity.Id,
                Role = Role.ADMIN,
                Token = token
            };

            return response;

        }

    }
}
