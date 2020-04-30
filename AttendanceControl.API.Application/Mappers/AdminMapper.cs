using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;

namespace AttendanceControl.API.Application.Mappers
{
    /// <summary>
    ///     Mapeos entre objetos AdminEntity y Admin
    /// </summary>
    public static class AdminMapper
    {
        /// <summary>
        ///     Mapea un objeto AdminEntity en un objeto Admin
        /// </summary>
        /// <param name="adminEntity"></param>
        /// <returns></returns>
        public static Admin Map(AdminEntity adminEntity)
        {
            return new Admin()
            {
                Id = adminEntity.Id,
                AdminName = adminEntity.AdminName,
                Password = adminEntity.Password
            };
        }

        /// <summary>
        ///     Mapea un objeto Admin en un objeto AdminEntity
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
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
