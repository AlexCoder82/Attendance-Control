using AttendanceControl.API.Business.Enums;
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

        public SchoolClassRepository(IAttendanceControlDBContext dbContext, ILogger<SchoolClassRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        ///     Cancela la vigencia de una clase: estable 
        ///     al campo IsCurrent el valor false
        /// </summary>
        /// <param name="schoolClassEntity"></param>
        /// <returns></returns>
        public async Task<bool> Cancel(int schoolClassId)
        {

            var id = new MySqlParameter("@schoolClassId", schoolClassId);

            _dbContext.Database.ExecuteSqlRaw("call cancel_school_class(@schoolClassId)", id);


            await _dbContext.SaveChangesAsync();


            return true;
        }

        /// <summary>
        ///     Recupera la lista de clases de un curso incluyendo
        ///     las asignaturas y los horarios
        /// </summary>
        /// <param name="courseId">
        ///     El id del curso
        /// </param>
        /// <returns>
        ///     Retorna la lista de clases
        /// </returns>
        public async Task<List<SchoolClassEntity>> GetByCourse(int courseId)
        {
            List<SchoolClassEntity> schoolClassEntities = await _dbContext.SchoolClassEntities
                .Include(sc => sc.SubjectEntity)
                .Include(sc => sc.ScheduleEntity)
                .Where(sc => sc.CourseId == courseId && sc.IsCurrent == true)
                .ToListAsync();

            _logger.LogInformation("La lista de clases ha sido recuperadada de la base de datos.");

            return schoolClassEntities;
        }

        public async Task<List<SchoolClassEntity>> GetByCourseAndSubject(int courseId, int subjectId)
        {
            List<SchoolClassEntity> schoolClassEntities = await _dbContext.SchoolClassEntities
              .Include(sc => sc.SubjectEntity)
              .Include(sc => sc.ScheduleEntity)
              .Include(sc => sc.SchoolClassStudentEntities)
              .Where(sc => sc.CourseId == courseId && sc.IsCurrent == true
                && sc.SubjectEntity.Id == subjectId)
              .ToListAsync();

            _logger.LogInformation("La lista de clases ha sido recuperadada de la base de datos.");

            return schoolClassEntities;
        }

        /// <summary>
        ///     Guarda una nueva clase 
        /// </summary>
        /// <param name="schoolClassEntity"></param>
        /// <returns>
        ///     Retorna la clase guardada con su id generado por la base de datos
        /// </returns>
        public async Task<SchoolClassEntity> Save(SchoolClassEntity schoolClassEntity)
        {
            await _dbContext.SchoolClassEntities.AddAsync(schoolClassEntity);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Nueva clase guardada en la base de datos.");

            return schoolClassEntity;
        }

        /// <summary>
        ///     Recupera una clase dado su id incluyendo 
        ///     la asignatura y el horario
        /// </summary>
        /// <param name="schoolClassId"></param>
        /// <returns>
        ///     Retorna la clase
        /// </returns>
        public async Task<SchoolClassEntity> GetCurrentIncludingSubjectAndSchedules(int schoolClassId)
        {
            SchoolClassEntity schoolClassEntity = await _dbContext.SchoolClassEntities
               .Include(sc => sc.SubjectEntity)
               .Include(sc => sc.ScheduleEntity)
               .FirstOrDefaultAsync(sc => sc.Id == schoolClassId);

            if (schoolClassEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado la clase de escuela, " +
                    "el id no existe en la tabla");
            }

            _logger.LogInformation("Classe vigente con su asignatura y horarios recuperada de la base de datos.");

            return schoolClassEntity;
        }

        /// <summary>
        ///     Actualiza la lista de alumnos que deben acudir a una clase
        /// </summary>
        /// <param name="schoolClassEntity">
        ///     El id de la clase 
        /// </param>
        /// <param name="studentEntities">
        ///     La lista de alumnos
        /// </param>
        /// <returns>
        ///     Retorna la clase modificada
        /// </returns>
        public async Task<SchoolClassEntity> UpdateStudents(SchoolClassEntity schoolClassEntity, List<StudentEntity>? studentEntities)
        {
            List<SchoolClassStudentEntity> schoolClassStudentEntities = new List<SchoolClassStudentEntity>();
            if (!(studentEntities is null))
            {
                studentEntities.ForEach(s =>
                {
                    schoolClassStudentEntities.Add(new SchoolClassStudentEntity()
                    {
                        StudentEntity = s,
                        SchoolClassEntity = schoolClassEntity
                    });
                });
            }
            schoolClassEntity.SchoolClassStudentEntities = schoolClassStudentEntities;

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Lista de alumnos de la clase de escuela actualizada.");

            return schoolClassEntity;
        }

        /// <summary>
        ///     Recupera una clase dado su id 
        /// </summary>
        /// <param name="schoolClassId"></param>
        /// <returns>
        ///     Retorna la clase
        /// </returns>
        public async Task<SchoolClassEntity> Get(int schoolClassId)
        {
            SchoolClassEntity schoolClassEntity = await _dbContext.SchoolClassEntities
                .Include(sc => sc.SchoolClassStudentEntities)
                .FirstOrDefaultAsync(sc => sc.Id == schoolClassId);

            _logger.LogInformation("Clase de escuela recuperadada de la base de datos.");

            return schoolClassEntity;
        }

        /// <summary>
        ///     Recupera la lista de clases a la hora actual,
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

            // TimeSpan now = new TimeSpan(12,50, 00);// DateTime.Now.TimeOfDay;

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
