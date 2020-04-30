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
    ///     Repositorio de cursos 
    /// </summary>
    public class CourseRepository : ICourseRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(
                    IAttendanceControlDBContext dbContext,
                    ILogger<CourseRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        ///     Obtiene la lista completa de entidades curso incluyendo el ciclo
        ///     al que peternecen
        /// </summary>
        /// <returns>
        ///     Retorna la lista de entidades curso
        /// </returns>
        public async Task<List<CourseEntity>> GetAll()
        {

            List<CourseEntity> courseEntities = await _dbContext.CourseEntities
                .Include(c => c.CycleEntity)
                .ToListAsync();

            _logger.LogInformation("Lista de cursos obtenida de la base de datos.");

            return courseEntities;

        }


        /// <summary>
        ///     Obtiene un curso por su Id, incluyendo 
        ///     las asignaturas asignadas 
        /// </summary>
        /// <param name="courseId"></param>
        /// <exception cref="DataNotFoundException">
        ///     Lanza DataNotFoundException si el id no existe
        /// </exception>
        /// <returns>
        ///     Retorna la entidad curso
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
        ///     Agrega una nueva asignatura a un curso
        ///     llamando un procedimiento de la base de datos que:
        ///     - inserta la relacion entre la asignatura y el curso
        ///     - inserta las relaciones entre la asignatura y cada alumno
        ///       que cursa el curso
        /// </summary>
        /// <param name="courseId">
        ///     El id del curso
        /// </param>
        /// <param name="subjectId">
        ///     El id de la asignatura
        /// </param>
        /// <exception cref="CourseSubjectDuplicateEntryException">
        ///     Lanza CourseSubjectDuplicateEntryException si se intenta
        ///     insertar una relacion entre el curso y la asignatura que
        ///     ya existe.
        /// </exception>
        /// <returns>
        ///     Retorna true
        /// </returns>
        public async Task<bool> AssignSubject(int courseId, int subjectId)
        {

            var course_id = new MySqlParameter("@courseId", courseId);
            var subject_id = new MySqlParameter("@subjectId", subjectId);

            try
            {
                _dbContext.Database.ExecuteSqlRaw("call add_subject_to_course(@subjectId,@courseId)", subject_id, course_id);

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("La asignatura ha sido agregada al curso");
            }
            catch (DbUpdateException ex)
            {
                //Si la base de datos lamza un error Unique Constraint si se asigna a un 
                //curso una asignatura que ya tiene asignada
                if (ex.InnerException.Message.Contains("UQ_course_subject"))
                {
                    _logger.LogWarning("Error: se ha intentado agregar a a un curso " +
                        "una asignatura que ya tiene");

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

        /// <summary>
        ///     Retira una asignatura de un curso llamando a
        ///     un procedimiento de la base de datos que :
        ///     -borra la relacion entre el curso y la asignatura 
        ///     -borra la relacion entre los alumnos que cursan el curso
        ///      y la asignatura
        ///     -borra la relacion entre los alumnos y las clases de la asignatura
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="subjectId"></param>
        /// <returns>
        ///     Retorna true
        /// </returns>
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
