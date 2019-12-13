using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    public class AdminRepository : IRepository<AdminEntity>, IAdminRepository
    {
        private readonly IAttendanceControlDBContext _dbBContext;

        public AdminRepository(IAttendanceControlDBContext attendanceControlDBContext)
        {
            _dbBContext = attendanceControlDBContext;
        }

        public Task<AdminEntity> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AdminEntity> Exists(AdminEntity adminEntity)
        {
            var result = await _dbBContext.AdminEntities
                .FirstOrDefaultAsync(a => a.AdminName == adminEntity.AdminName
                    && a.Password == adminEntity.Password);

            return result;
        }

        public Task<AdminEntity> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AdminEntity> Save(AdminEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<AdminEntity> Update(int id, AdminEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
