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
    public class CourseRepository : ICourseRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(IAttendanceControlDBContext dbContext,
                                ILogger<CourseRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        ///     Recupera la lista completa de cursos incluyendo el ciclo
        ///     al que peternecen
        /// </summary>
        /// <returns>
        ///     Retorna la lista de cursos
        /// </returns>
        public async Task<List<CourseEntity>> GetAll()
        {
            List<CourseEntity> courseEntities = await _dbContext.CourseEntities
                .Include(c => c.CycleEntity)

                .ToListAsync();

            _logger.LogInformation("Lista de cursos recuperada de la base de datos.");

            return courseEntities;
        }


        /// <summary>
        ///     Recupera un curso por su Id, incluyendo 
        ///     las asignaturas asignadas 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>
        ///     Retorna el curso o lanza
        ///     DataNotFoundException si el id no existe en la tabla
        /// </returns>
        public async Task<CourseEntity> GetIncludingAssignedSubjects(int courseId)
        {
            CourseEntity courseEntity = await _dbContext.CourseEntities
                    .Include(c => c.CourseSubjectEntities)
                    .FirstOrDefaultAsync(c => c.Id == courseId);

            if (courseEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado el curso " +
                    "en la base de datos, el Id no existe.");
            }

            _logger.LogInformation("Curso recuperado de la base de datos.");

            return courseEntity;
        }

        /// <summary>
        ///     Recupera un curso dado un id, incluyendo
        ///     el ciclo al que pertenece y las asignaturas
        ///     asignadas
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>
        ///     Retorna el curso o lanza DataNotFoundException
        ///     si el id no existe en la tabla
        /// </returns>
        public async Task<CourseEntity> GetIncludingCycleAndAssignedSubjects(int courseId)
        {
            CourseEntity entityToUpdate = await _dbContext.CourseEntities
                    .Include(c => c.CourseSubjectEntities).ThenInclude(cs => cs.SubjectEntity)
                    .Include(c => c.CycleEntity)
                    .FirstOrDefaultAsync(c => c.Id == courseId);

            if (entityToUpdate is null)
            {
                throw new DataNotFoundException("No se ha encontrado el curso " +
                    "en la base de datos, el id no existe.");
            }

            _logger.LogInformation("Curso recuperado de la base de datos.");

            return entityToUpdate;
        }

        /// <summary>
        ///     Actualiza la lista de asignaturas asignadas a un curso
        /// </summary>
        /// <param name="courseEntity">
        ///     El curso incluyendo las nuevas asignaturas
        /// </param>
        /// <returns>
        ///     El aciclo actualizado o lanza CourseSubjectDuplicateEntryException si 
        ///     se intenta asignar una asignatura ya asignada
        /// </returns>
        public async Task<bool> AssignSubject(int courseId, int subjectId)
        {
            var course_id = new MySqlParameter("@courseId", courseId);
            var subject_id = new MySqlParameter("@subjectId", subjectId);

            try
            {
                _dbContext.Database.ExecuteSqlRaw("call add_subject_to_course(@subjectId,@courseId)", subject_id, course_id);

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Asignaturas del curso actualizadas.");
            }
            catch (DbUpdateException ex)
            {
                //Si la base de datos lamza un error Unique Constraint si se asigna a un 
                //curso una asignatura que ya tiene asignada
                if (ex.InnerException.Message.Contains("UQ_course_subject"))
                {
                    _logger.LogWarning("No se ha asignado la asignatura porque el curso ya la tiene asignada");

                    throw new CourseSubjectDuplicateEntryException();
                }
                //Por cualquier otra razón, se lanza la excepción
                else
                {
                    throw ex;
                }
            }

            return true;
        }

        public async Task<bool> RenoveAssignedSubject(int courseId, int subjectId)
        {

            var course_id = new MySqlParameter("@courseId", courseId);
            var subject_id = new MySqlParameter("@subjectId", subjectId);


            _dbContext.Database.ExecuteSqlRaw("call remove_subject_from_course(@subjectId,@courseId)", subject_id, course_id);

            await _dbContext.SaveChangesAsync();


            _logger.LogInformation("Asignaturas del curso actualizadas.");

            return true;
        }
    }
}
