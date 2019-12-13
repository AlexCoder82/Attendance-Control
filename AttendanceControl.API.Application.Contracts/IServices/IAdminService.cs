using AttendanceControl.API.Business.Models;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    public interface IAdminService
    {     
        public Task<Admin> SignIn(Admin admin);
        public Task<Admin> Update(int id, Admin admin);
    }
}
