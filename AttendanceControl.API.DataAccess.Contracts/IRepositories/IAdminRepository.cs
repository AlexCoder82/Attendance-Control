
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    /// <summary>
    ///      Contract for administrator table operations
    /// </summary>
    public interface IAdminRepository
    {
        /// <summary>
        ///     Check if Admin exists (Signin)
        /// </summary>
        /// <param name="adminEntity">
        ///     AdminEntity instance with admin name and password properties
        /// </param>
        /// <returns>
        ///     if Admin exists,method returns the found instance of AdminEntity, 
        ///     else method throws WrongCredentialsException 
        /// </returns>
        public Task<AdminEntity> Exists(AdminEntity adminEntity);//Throw WrongCredentialsException 
    }
}
