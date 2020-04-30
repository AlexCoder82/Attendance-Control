using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    /// <summary>
    ///     Clase que contiene los métodos de acceso a la tabla de los ciclos
    /// </summary>
    public class CycleRepository : ICycleRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<CycleRepository> _logger;

        public CycleRepository(IAttendanceControlDBContext dbContext,
                               ILogger<CycleRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

       


        /// <summary>
        ///     Recupera un ciclo de la base de datos por su Id
        /// </summary>
        /// <param name="id">
        ///     El id del ciclo
        /// </param>
        /// <returns>
        ///     Retona el ciclo encontrado o lanza 
        ///     DataNotFoundException si el id no existe en la tabla
        /// </returns>
        public async Task<CycleEntity> Get(int id)
        {
            var cycleEntity = await _dbContext.CycleEntities.FirstOrDefaultAsync(c => c.Id == id);

            if (cycleEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado el ciclo en la base de datos, el Id no existe.");
            }

            _logger.LogInformation("El ciclo ha sido recuperado de la base de datos.");

            return cycleEntity;
        }

        /// <summary>
        ///     Recupera un ciclo de la base de datos por su Id,
        ///     incluyendo los dos cursos y las asignaturas 
        ///     asignadas a los dos cursos;
        /// </summary>
        /// <param name="id">
        ///     El id del ciclo
        /// </param>
        /// <returns>
        ///     Retona el ciclo encontrado o lanza 
        ///     DataNotFoundException si el id no existe en la tabla
        /// </returns>
        public async Task<CycleEntity> GetIncludingCoursesAndAssignedSubjects(int id)
        {
            var cycleEntity = await _dbContext.CycleEntities
                .Include(c => c.CourseEntities)
                .ThenInclude(co => co.CourseSubjectEntities)
                .ThenInclude(cs => cs.SubjectEntity)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cycleEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado " +
                    "el ciclo en la base de datos, el Id no existe.");
            }

            _logger.LogInformation("El ciclo ha sido recuperado " +
                "de la base de datos.");

            return cycleEntity;
        }

        /// <summary>
        ///      Recupera la lista de ciclos de la base de datos,
        ///      incluyendo los cursos y las asignaturas ;
        /// </summary>
        /// <returns>
        ///     Retorna la lista de los ciclos 
        /// </returns>
        public async Task<List<CycleEntity>> GetAllIncludingCoursesSubjectsAndSchedules()
        {
            List<CycleEntity> cycleEntities = await _dbContext.CycleEntities
                .Include(c=>c.ShiftEntity)
                .ThenInclude(s=>s.ScheduleEntities)
                .Include(c => c.CourseEntities)
                .ThenInclude(co => co.CourseSubjectEntities)
                .ThenInclude(cs => cs.SubjectEntity)
                .OrderBy(c => c.Name)
                .ToListAsync();

            _logger.LogInformation("Lista de ciclos recuperada " +
                "de la base de datos.");

           

            return cycleEntities;
        }
      
        /// <summary>
        ///     Guarda un ciclo nuevo en la base de datos
        /// </summary>
        /// <param name="cycleEntity">
        ///     El ciclo nuevo a guardar
        /// </param>
        /// <returns>
        ///     Retorna el mismo ciclo pero con su Id 
        ///     generado por la base de datos
        ///     o lanza GradeNameDuplicateEntryException
        ///     si el nombre del ciclo ya existe en la tabla
        /// </returns>
        public async Task<CycleEntity> Save(CycleEntity cycleEntity)
        {
            try
            {
                await _dbContext.CycleEntities.AddAsync(cycleEntity);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Nuevo ciclo guardado en la base datos.");
            }
            catch (DbUpdateException ex)
            {
                //Si la base de datos lamza un error Unique Constraint por el campo "name"
                if (ex.InnerException.Message.Contains("UQ_cycle_name"))
                {
                    _logger.LogWarning("El ciclo no se ha guardado " +
                        "porque su nombre ya existe");
                  
                    throw new GradeNameDuplicateEntryException();
                }
                //Por cualquier otra razón, se lanza la excepción
                else
                {
                    throw ex;
                }
            }

            return cycleEntity;
        }

        /// <summary>
        ///     Actualiza el nombre de un ciclo
        /// </summary>
        /// <param name="cycleId">
        ///     El id del ciclo
        /// </param>
        /// <param name="name">
        ///     El nuevo nombre a asignar
        /// </param>
        /// <returns>
        ///     Retorna true si se actualiza correctamente o 
        ///     lanza GradeNameDuplicateEntryException 
        ///     si el nuevo nombre ya existe en la tabla    
        /// </returns>
        public async Task<bool> Update(CycleEntity cycleEntity)
        {
            CycleEntity c = await this.Get(cycleEntity.Id);

            try
            {
                c.Name = cycleEntity.Name;
                c.ShiftId = c.ShiftId;
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("El ciclo se ha actualizado correctamente");
            }
            catch (DbUpdateException ex)
            {
                //Si la base de datos lamza un error Unique Constraint por el campo "name"
                if (ex.InnerException.Message.Contains("UQ_cycle_name"))
                {
                    _logger.LogWarning("El ciclo no se ha actualizado " +
                        "porque su nombre ya existe");
                  
                    throw new GradeNameDuplicateEntryException();
                }
                //Por cualquier otra razón, se lanza la excepción
                else
                {
                    throw ex;
                }
            }

            return true;
        }

    }
}
