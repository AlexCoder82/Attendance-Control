using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    /// <summary>
    ///     Repositorio de ausencias
    /// </summary>
    public class AbsenceRepository : IAbsenceRepository
    {

        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<AbsenceRepository> _logger;

        public AbsenceRepository(
                    IAttendanceControlDBContext dbContext,
                    ILogger<AbsenceRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        ///     Obtiene un registro de ausencia por su id
        /// </summary>
        /// <param name="absenceId"></param>
        /// <exception cref="DataNotFoundException">
        ///     Lanza DataNotFoundException si el id no existe
        /// </exception>
        /// <returns>
        ///     Retorna una entidad AbsenceEntity
        /// </returns>
        public async Task<AbsenceEntity> Get(int absenceId)
        {

            AbsenceEntity absenceEntity = await _dbContext.AbsenceEntities.FirstOrDefaultAsync(a => a.Id == absenceId);

            if (absenceEntity is null)
            {
                var message = "No se ha encontrado la ausencia, el id no existe";

                throw new DataNotFoundException(message);
            }

            _logger.LogInformation("Ausencia con id " + absenceId + " obetenida de la base de datos");

            return absenceEntity;

        }

        /// <summary>
        ///     Obtiene la lista de ausencias de un alumno 
        ///     incluyendo la asignatura y el horario de las clases
        ///     perdidas
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno
        /// </param>
        /// <returns>
        ///     Retorna una lista de entidades AbsenceEntity
        /// </returns>
        public async Task<List<AbsenceEntity>> GetByStudent(int studentId)
        {

            List<AbsenceEntity> entities = await _dbContext.AbsenceEntities
                .Include(a => a.SchoolClassEntity).ThenInclude(sc => sc.ScheduleEntity)
                .Include(a => a.SchoolClassEntity)
                    .ThenInclude(sc => sc.SubjectEntity)
                .Where(a => a.StudentId == studentId)
                .ToListAsync();

            _logger.LogInformation("Lista de ausencias del alumno con id "
                + studentId + " obtenidas de la base de datos");

            return entities;

        }

        /// <summary>
        ///     Obtiene la ausencia de un alumno dado 
        ///     su id ,el id de una clase y el día de hoy
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="schoolClassId"></param>
        /// <returns>
        ///     Retorna una entidad AbsenceEntity
        /// </returns>
        public async Task<AbsenceEntity> GetByStudentAndSchoolClass(int studentId, int schoolClassId)
        {

            AbsenceEntity entity =  await _dbContext.AbsenceEntities
                .FirstOrDefaultAsync(a => a.StudentId == studentId
                        && a.SchoolClassId == schoolClassId
                        && a.Date == DateTime.Today);

            _logger.LogInformation("Ausencia del alumno con id "
                + studentId + " de la clase con id " + schoolClassId + " obtenida");

            return entity;

        }

        /// <summary>
        ///     Guarda varias ausencias en la base de datos 
        ///     con un bucle que llama un procedimiento que comprueba 
        ///     si la ausencia ya existe, en tal caso 
        ///     la borra si es cancelada o la modifica si es de otro tipo.
        ///     En el caso de que la ausencia no existe, inserta una nueva.
        ///     El procedimiento tambien dispara un trigger que actualiza las
        ///     ausencias y retrasos totales del alumno.
        /// </summary>
        /// <param name="absenceEntities"></param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        public async Task<bool> Save(List<AbsenceEntity> absenceEntities)
        {

            absenceEntities.ForEach(a =>
            {
                var schoolClassId = new MySqlParameter("@schoolClassId", a.SchoolClassId);
                var studentId = new MySqlParameter("@studentId", a.StudentId);
                var type = new MySqlParameter("@type", a.Type);

                _dbContext.Database.ExecuteSqlRaw("call insert_absence(@schoolClassId,@studentId,@type)",
                             schoolClassId, studentId, type);

            });

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Ausencias guardas en la base de datos");

            return true;

        }

        /// <summary>
        ///     Justifica o no una ausencia modificando
        ///     el campo "isExcused" de una entidad ausencia
        /// </summary>
        /// <param name="absenceEntity"></param>
        /// <param name="isExcused"></param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        public async Task<bool> SetExcused(AbsenceEntity absenceEntity, bool isExcused)
        {

            absenceEntity.IsExcused = isExcused;
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Justificacion de la ausencia con id " 
                + absenceEntity.Id + " actualizada");

            return true;

        }

    }
}
