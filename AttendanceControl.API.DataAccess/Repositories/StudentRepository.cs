using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    /// <summary>
    ///     Repositorio de alumnos
    /// </summary>
    public class StudentRepository : IStudentRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<StudentRepository> _logger;

        public StudentRepository(IAttendanceControlDBContext dbContext,
                                 ILogger<StudentRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        ///     Recupera una entidad alumno por id
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno
        /// </param>
        /// <exception cref="DataNotFoundException">
        ///     Lanza DataNotFoundException si el id no existe
        /// </exception>
        /// <returns>
        ///     Retorna la entidad alumno 
        /// </returns>
        public async Task<StudentEntity> Get(int studentId)
        {

            StudentEntity studentEntity = await _dbContext.StudentEntities
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (studentEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado el alumno en " +
                    "la base de datos, el Id no existe.");
            }

            _logger.LogInformation("Alumno recuperado de la base de datos.");

            return studentEntity;

        }
    

        /// <summary>
        ///     Recupera una lista de entidades alumnos por página y cuyo primer apellido 
        ///     empieza por la cadena lastname introducida por parametro 
        ///     incluyendo el curso y las asignaturas que cursa
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns>
        ///     Retorna la lista de entidades alumno
        /// </returns>
        public async Task<List<StudentEntity>> GetByPageLikeLastNameIncludingCourseAndSubjects(string lastName, int page)
        {

            List<StudentEntity> studentEntities = await _dbContext.StudentEntities
                .Include(s => s.CourseEntity)
                    .ThenInclude(c => c.CycleEntity)
                .Include(s => s.StudentSubjectEntities)
                    .ThenInclude(ss => ss.SubjectEntity)
                    .Where(s => s.LastName1
                    .ToLower()
                    .StartsWith(lastName.ToLower()))
                .OrderBy(s => s.LastName1).ThenBy(s => s.LastName2)
                .Skip((page - 1) * 8)
                .Take(8)
                .ToListAsync();

            _logger.LogInformation("Lista de alumnos recuperada de la base de datos.");

            return studentEntities;
        }


        /// <summary>
        ///     Inserta una nueva ebtidad alumno 
        /// </summary>
        /// <param name="studentEntity"></param>
        /// <exception cref="DniDuplicateEntryException">
        ///     Lanza DniDuplicateEntryException si el dni del alumno ya existe
        /// </exception>
        /// <returns>
        ///     Retorna la entidad alumno guardada con su id generado 
        /// </returns>
        public async Task<StudentEntity> Save(StudentEntity studentEntity)
        {

            try
            {
                await _dbContext.StudentEntities.AddAsync(studentEntity);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Alumno guardado en la base de datos.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_dni"))
                {
                    _logger.LogWarning("El alumno no se ha guardado " +
                        "porque su dni ya existe en la base de datos");

                    throw new DniDuplicateEntryException();
                }
                //Por cualquier otra razon lanzo la excepción
                else
                {
                    throw ex;
                }
            }


            return studentEntity;

        }

        /// <summary>
        ///     Actualiza una entidad alumno
        /// </summary>
        /// <param name="studentEntity">
        ///     La entidad alumno con los nuevos datos
        /// </param>
        /// <exception cref="DniDuplicateEntryException">
        ///     Lanza DniDuplicateEntryException si el dni del alumno ya existe
        /// </exception>
        /// <returns>
        ///     Retorna la entidad alumno actualizada
        /// </returns>
        public async Task<StudentEntity> Update(StudentEntity studentEntity)
        {

            StudentEntity studentToUpdate = await this.Get(studentEntity.Id);
            studentToUpdate.LastName1 = studentEntity.LastName1;
            studentToUpdate.LastName2 = studentEntity.LastName2;
            studentToUpdate.FirstName = studentEntity.FirstName;
            studentToUpdate.Dni = studentEntity.Dni;

            try
            {
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Datos del Alumno actualizados en la base de datos.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_dni"))
                {
                    _logger.LogWarning("El alumno no se ha actualizado " +
                        "porque su dni ya existe en la base de datos");

                    throw new DniDuplicateEntryException();
                }
                //Por cualquier otra razon lanzo la excepción
                else
                {
                    throw ex;
                }
            }

            return studentEntity;

        }

        /// <summary>
        ///     Inserta una relacion entre una entidad curso
        ///     y una entidad alumno llamando a un
        ///     procedimiento de la base de datos que:
        ///     -Actualiza el id del curso en la tabla alumno
        ///     -Borra las posibles relaciones entre el alumno y 
        ///      las asignaturas que podria estar cursando hasta ahora
        ///     -Inserta las nuevas relaciones entre el alumno y las 
        ///      asignaturas del nuevo curso
        ///     -Borra las relaciones entre el alumno y las clases 
        ///     -Inserta las relaciones entre el alumno y las clases del nuevo curso
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno
        /// </param>
        /// <param name="courseId">
        ///     El id del curso 
        /// </param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        public async Task<bool> UpdateCourse(int studentId, int courseId)
        {

            var course_id = new MySqlParameter("@courseId", courseId);
            var student_id = new MySqlParameter("@studentId", studentId);


            _dbContext.Database.ExecuteSqlRaw("call add_student_to_course(@studentId,@courseId)", student_id, course_id);

            await _dbContext.SaveChangesAsync();


            _logger.LogInformation("Curso cursado por el Alumno actualizado en la base de datos.");

            return true;

        }

        /// <summary>
        ///     Establece el valor null al id del curso en la tabla
        ///     alumno llamando un procedimiento que:
        ///     -estable el valor null al id del curso
        ///     -borra las relaciones entre el alumno y las asignaturas del curso
        ///     -borra las relaciones entre el alumno y las clases del curso
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        public async Task<bool> RemoveCourse(int studentId)
        {

            var student_id = new MySqlParameter("@studentId", studentId);

            _dbContext.Database.ExecuteSqlRaw("call remove_student_from_course(@studentId)", student_id);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("El alumno con id " + studentId + " ya no tiene curso asignado");

            return true;

        }


        /// <summary>
        ///     Actualiza las asignaturas cursadas por un alumno 
        ///     llamando a un procedimiento de la base de datos que:
        ///     -borra las relaciones entre el alumno y las asignaturas que cursa
        ///     -borra las relaciones entre el alumno y las clases 
        ///     -inserta las relaciones entre el alumno y las nuevas asignaturas 
        ///     -inserta las relaciones entre el alumno y las clases de las nuevas asignaturas
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno 
        /// </param>
        /// <param name="subjectIds">
        ///     Un array con los ids de las asignaturas que
        ///     va a cursar el alumno
        /// </param>
        /// <returns>
        ///     Retorna true 
        /// </returns>
        public async Task<bool> UpdateSubjects(int studentId, int[] subjectIds)
        {

            //Paso el array en formato JSON al procedimiento
            _dbContext.Database.ExecuteSqlRaw("call update_student_assigned_subjects({0},{1})",
                            studentId, JsonConvert.SerializeObject(subjectIds));

            await _dbContext.SaveChangesAsync();

            return true;

        }



       



        public async Task<List<StudentEntity>> GetByCurrentSchoolClass(int schoolClassId)
        {

            List<StudentEntity> studentEntities = await _dbContext.StudentEntities
                .Join(_dbContext.SchoolClassStudentEntities, s => s.Id, scs => scs.StudentId, (s, scs) => new { s, scs })
                .Join(_dbContext.SchoolClassEntities, sscs => sscs.scs.SchoolClassId, sc => sc.Id, (sscs, sc) => new { sscs, sc })
                .Where(x => x.sc.IsCurrent == true && x.sc.Id == schoolClassId)
                 .Select(x => x.sscs.s)
                .ToListAsync();

            return studentEntities;

        }

        public async Task<List<StudentEntity>> GetByPageIncludingCourseAndSubjects(int page)
        {

            List<StudentEntity> studentEntities = await _dbContext.StudentEntities
                .Include(s => s.CourseEntity)
                    .ThenInclude(c => c.CycleEntity)
                .Include(s => s.StudentSubjectEntities)
                    .ThenInclude(ss => ss.SubjectEntity)
                .OrderBy(s => s.LastName1).ThenBy(s => s.LastName2)
                .Skip((page - 1) * 8)
                .Take(8)
                .ToListAsync();

            return studentEntities;



        }

        public async Task<StudentEntity> GetIncludingSubjects(int studentId)
        {
            StudentEntity studentEntity = await _dbContext.StudentEntities
                .Include(s => s.StudentSubjectEntities).ThenInclude(ss => ss.SubjectEntity)
                .FirstOrDefaultAsync(s => s.Id == studentId);


            _logger.LogInformation("El alumno ha sido recuperado de la base de datos.");

            return studentEntity;
        }

    }
}
