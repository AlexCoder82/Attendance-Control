using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using AttendanceControl.API.DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    /// <summary>
    ///     Repositorio de administradores
    /// </summary>
    public class AdminRepository : IAdminRepository
    {
        private readonly IAttendanceControlDBContext _dbBContext;
        private readonly ILogger<AdminRepository> _logger;

        public AdminRepository(
                    IAttendanceControlDBContext dbContext,
                    ILogger<AdminRepository> logger)
        {
            _dbBContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        ///     Comprueba si los credenciales de un
        ///     administrador existen en la base de datos
        /// </summary>
        /// <param name="adminEntity"></param>
        /// <exception cref="WrongCredentialsException">
        ///     Lanza WrongCredentialsException si no existen
        /// </exception>
        /// <returns>
        ///     Retorna la entidad administrador
        /// </returns>
        public async Task<AdminEntity> Exists(AdminEntity adminEntity)//Throw WrongCredentialsException
        {

            var adminNameMD5 = MD5handler.GenerateMD5(adminEntity.AdminName);
            var passwordMD5 = MD5handler.GenerateMD5(adminEntity.Password);

            AdminEntity result = await _dbBContext.AdminEntities
                .FirstOrDefaultAsync(a => a.AdminName == adminNameMD5 && a.Password == passwordMD5);

            _logger.LogInformation("Administrador reconocido");

            if (result is null)
            {
                throw new WrongCredentialsException();
            }

            return result;

        }

    }
}
