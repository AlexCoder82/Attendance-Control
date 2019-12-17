using AttendanceControl.API.Business.Models;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    public interface IAdminService
    {     
        public Task<string> SignIn(Admin admin);
    }
}
