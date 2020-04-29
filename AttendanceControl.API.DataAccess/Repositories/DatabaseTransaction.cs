

using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    /// <summary>
    ///     Clase con los métodos de transacciones de la base de datos
    /// </summary>
    public class DatabaseTransaction : IDatabaseTransaction
    {

        private readonly AttendanceControlDBContext _dbContext;
        private readonly ILogger<DatabaseTransaction> _logger;

        public DatabaseTransaction(AttendanceControlDBContext dbContext,
                                   ILogger<DatabaseTransaction> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        ///     Abre una transacción
        /// </summary>
        /// <returns></returns>
        public async Task Begin()
        {
            await _dbContext.Database.BeginTransactionAsync();

            _logger.LogInformation("Transacción empezada");
        }

        /// <summary>
        ///     Realiza una transacción
        /// </summary>
        public void Commit()
        {
            _dbContext.Database.CommitTransaction();

            _logger.LogInformation("Transacción terminada con éxito");
        }

        /// <summary>
        ///     Cancela una transacción abierta
        /// </summary>
        public void Rollback()
        {
            _dbContext.Database.RollbackTransaction();

            _logger.LogInformation("Transacción cancelada: se realiza un rollback");
        }
    }
}
