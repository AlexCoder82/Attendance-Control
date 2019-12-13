using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;

namespace AttendanceControl.API.Application.Mappers
{
    public static class AdminMapper
    {
        public static Admin Map(AdminEntity adminEntity)
        {
            return new Admin()
            {
                Id = adminEntity.Id,
                AdminName = adminEntity.AdminName,
                Password = adminEntity.Password
            };
        }

        public static AdminEntity Map(Admin admin)
        {
            return new AdminEntity()
            {
                Id = admin.Id,
                AdminName = admin.AdminName,
                Password = admin.Password
            };
        }
    }
}
