
using System.Threading.Tasks;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.Mappers;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;

namespace AttendanceControl.API.Application.Services
{    
    public class AdminService:IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<Admin> SignIn(Admin admin)
        {
            AdminEntity adminEntity = AdminMapper.Map(admin);

            var signedInAdminEntity = await _adminRepository.Exists(adminEntity);

            return AdminMapper.Map(signedInAdminEntity);
        }

        public Task<Admin> Update(int id, Admin admin)
        {
            throw new System.NotImplementedException();
        }
    }
}
