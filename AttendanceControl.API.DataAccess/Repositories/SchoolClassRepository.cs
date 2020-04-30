using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    /// <summary>
    ///     Clase con los métodos de acceso a la tabla de clases 
    /// </summary>
    public class SchoolClassRepository : ISchoolClassRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<SchoolClassRepository> _logger;

        public SchoolClassRepository(IAttendanceControlDBContext dbContext,
                                     ILogger<SchoolClassRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        ///     Cancela la vigencia de una clase llamando a un
        ///     procedimiento de la base de datos que:
        ///     -cancela la clase estableciendo el valor false al
        ///      campo "isCurrent"
        ///     -Borra todas las relaciones entre los alumnos y la clase
        /// </summary>
        /// <param name="schoolClassEntity">
        ///     El id de la clase
        /// </param>
        /// <returns><
        ///     Retorna true
        /// /returns>
        public async Task<bool> Cancel(int schoolClassId)
        {

            var id = new MySqlParameter("@schoolClassId", schoolClassId);

            _dbContext.Database.ExecuteSqlRaw("call cancel_school_class(@schoolClassId)", id);

            await _dbContext.SaveChangesAsync();

            return true;

        }

        /// <summary>
        ///     Recupera la lista de entidades clase de un curso incluyendo
        ///     las asignaturas y los horarios.
        /// </summary>
        /// <param name="courseId">
        ///     El id del curso
        /// </param>
        /// <returns>
        ///     Retorna la lista de entidades clase
        /// </returns>
        public async Task<List<SchoolClassEntity>> GetByCourse(int courseId)
        {

            List<SchoolClassEntity> schoolClassEntities = await _dbContext.SchoolClassEntities
                .Include(sc => sc.SubjectEntity)
                .Include(sc => sc.ScheduleEntity)
                .Where(sc => sc.CourseId == courseId && sc.IsCurrent == true)
                .ToListAsync();

            _logger.LogInformation("La lista de clases ha sido obtenida de la base de datos.");

            return schoolClassEntities;

        }

    

        /// <summary>
        ///     Guarda una nueva entidad clase 
        /// </summary>
        /// <param name="schoolClassEntity"></param>
        /// <returns>
        ///     Retorna la entidad clase guardada con su id generado por la base de datos
        /// </returns>
        public async Task<SchoolClassEntity> Save(SchoolClassEntity schoolClassEntity)
        {

            await _dbContext.SchoolClassEntities.AddAsync(schoolClassEntity);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Nueva clase guardada en la base de datos.");

            return schoolClassEntity;

        }
    

        /// <summary>
        ///     Recupera la lista de entidades clase a la hora actual,
        ///     el dia de la semana actual
        ///     dado el id del profesor que imparte las clases (Listado de alumnos) 
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns>
        ///     Retorna la lista de clases 
        /// </returns>
        public async Task<List<SchoolClassEntity>> GetByTeacher(int teacherId)
        {

            DayOfWeek day = DayOfWeek.Thursday;

            List<SchoolClassEntity> schoolClassEntities = await _dbContext.SchoolClassEntities
                .Include(sc => sc.SubjectEntity)
                .Include(sc => sc.ScheduleEntity)
                .Where(sc => sc.Day == day
                    && sc.IsCurrent == true
                    && sc.SubjectEntity.TeacherId == teacherId
                )
                .ToListAsync();

            return schoolClassEntities;

        }

    }
}
