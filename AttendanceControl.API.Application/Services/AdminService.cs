
using System.Threading.Tasks;
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
    public class AdminService:IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthService _authService;
        private ILogger<AdminService> _logger;

        public AdminService(IAdminRepository adminRepository, IAuthService authService, ILogger<AdminService> logger)
        {
            _adminRepository = adminRepository;
            _authService = authService;
            _logger = logger;
        }

        public async Task<string> SignIn(Admin admin)//Throw WrongCredentialsException
        {
            AdminEntity adminEntity = AdminMapper.Map(admin);

            var signedInAdmin = await _adminRepository.Exists(adminEntity);
          
            string token = _authService
                    .GenerateToken(signedInAdmin.AdminName, signedInAdmin.Password, Role.ADMIN);

            return token;
        }

    }
}
