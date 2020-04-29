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
    ///     Clase con los métodos de acceso a la tabla de alumnos
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
        ///     Recupera un alumno con el id introducido por parámetro
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno
        /// </param>
        /// <returns>
        ///     Retorna la entidad alumno con sus datos personales o  lanza 
        ///     DataNotFoundException si el id no existe en la tabla
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
        ///     Recupera el alumno con el id introducido por parámetro 
        ///     incluyendo el curso y las asignaturas que cursa
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno
        /// </param>
        /// <returns>
        ///     Retorna la entidad alumno o lanza DataNotFoundException 
        ///     si no existe el id en la tabla
        /// </returns>
        public async Task<StudentEntity> GetIncludingCourseAndSubjectsAndSchoolCLasses(int studentId)
        {
            StudentEntity studentEntity = await _dbContext.StudentEntities
                .Include(s => s.CourseEntity).ThenInclude(c => c.CycleEntity)
                .Include(s => s.StudentSubjectEntities).ThenInclude(ss => ss.SubjectEntity)
                .Include(s => s.SchoolClassStudentEntities)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (studentEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado el alumno en " +
                    "la base de datos, el Id no existe.");
            }

            _logger.LogInformation("El alumno ha sido recuperado de la base de datos.");

            return studentEntity;
        }

        /// <summary>
        ///     Recupera el alumno con el id introducido por parámetro
        ///     incluyendo el curso que cursa
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>
        ///     Retorna la entidad alumno o lanza DataNotFoundException
        ///     si no existe el id en la tabla
        /// </returns>
        public async Task<StudentEntity> GetIncludingCourse(int studentId)
        {
            StudentEntity studentEntity = await _dbContext.StudentEntities
                .Include(s => s.CourseEntity)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (studentEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado el alumno en " +
                    "la base de datos, el Id no existe.");
            }

            _logger.LogInformation("El alumno ha sido recuperado de la base de datos.");

            return studentEntity;
        }


        /// <summary>
        ///     Recupera una lista de alumnos cuyo primer apellido 
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
        ///     Guarda un nuevo alumno en la base de datos
        /// </summary>
        /// <param name="studentEntity"></param>
        /// <returns>
        ///     Retorna la entidad alumno guardada con su id generado por la base de datos
        /// </returns>
        public async Task<StudentEntity> Save(StudentEntity studentEntity)
        {
            await _dbContext.StudentEntities.AddAsync(studentEntity);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Alumno guardado en la base de datos.");

            return studentEntity;
        }

        /// <summary>
        ///     Actualiza los datos personales de un alumno 
        /// </summary>
        /// <param name="studentEntity">
        ///     La entidad alumno con los nuevos datos
        /// </param>
        /// <returns>
        ///     Retorna el alumno actualizado
        /// </returns>
        public async Task<StudentEntity> Update(StudentEntity studentEntity)
        {
            StudentEntity studentToUpdate = await this.Get(studentEntity.Id);
            studentToUpdate.LastName1 = studentEntity.LastName1;
            studentToUpdate.LastName2 = studentEntity.LastName2;
            studentToUpdate.FirstName = studentEntity.FirstName;
            studentToUpdate.Dni = studentEntity.Dni;

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Datos del Alumno actualizados en la base de datos.");

            return studentEntity;
        }

        /// <summary>
        ///     Actualiza el curso cursado por un alumno
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno
        /// </param>
        /// <param name="courseId">
        ///     El id del curso que va a cursar(opcional)
        /// </param>
        /// <returns>
        ///     Retorna el alumno incluyendo el curso asignado
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

        public async Task<bool> RemoveCourse(int studentId)
        {
            var student_id = new MySqlParameter("@studentId", studentId);

            _dbContext.Database.ExecuteSqlRaw("call remove_student_from_course(@studentId)", student_id);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("El alumno con id " + studentId + " ya no tiene curso asignado");

            return true;
        }


        public async Task<List<StudentSubjectEntity>> GetAssignedSubjects(int studentId)
        {
            List<StudentSubjectEntity> studentSubjectEntities = await _dbContext.StudentSubjectEntities
                .Where(ss => ss.StudentId == studentId)
                        .ToListAsync();

            return studentSubjectEntities;


        }

        /// <summary>
        ///     Actualiza las asignaturas cursadas por un alumno, llama a un procedimiento
        ///     que borra las relaciones entre el alumno y las antiguas asignatira y clases, 
        ///     y crea las nuevas relaciones.
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno 
        /// </param>
        /// <param name="subjectIds">
        ///     Un array con los ids de las asignaturas que
        ///     va a cursar el alumno
        /// </param>
        /// <returns>
        ///     Retorna true o lanza excepción si falla la transacción 
        /// </returns>
        public async Task<bool> UpdateSubjects(int studentId, int[] subjectIds)
        {

            _dbContext.Database.ExecuteSqlRaw("call update_student_assigned_subjects({0},{1})", 
                            studentId, JsonConvert.SerializeObject(subjectIds));

            await _dbContext.SaveChangesAsync();

            return true;


        }


        public async Task<bool> AddSubject(StudentEntity studentEntity, SubjectEntity subjectEntity)
        {
            studentEntity.StudentSubjectEntities.Add(new StudentSubjectEntity()
            {
                StudentEntity = studentEntity,
                SubjectEntity = subjectEntity
            });

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Asignaturas cursadas por el alumno " +
                "actualizadas en la base de datos.");

            return true;
        }

        public async Task<bool> RemoveSubject(StudentEntity studentEntity, SubjectEntity subjectEntity)
        {
            StudentSubjectEntity studentSubjectEntity = studentEntity.StudentSubjectEntities
                .FirstOrDefault(ss => ss.SubjectEntity == subjectEntity);
            studentEntity.StudentSubjectEntities.Remove(studentSubjectEntity);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Asignaturas cursadas por el alumno " +
                "actualizadas en la base de datos.");

            return true;
        }

        /// <summary>
        ///     Actualiza las clases a las que el alumno debe acudir
        /// </summary>
        /// <param name="studentEntity">
        ///     La entidad alumno
        /// </param>
        /// <param name="schoolClassEntities">
        ///     La lista de entidades clase
        /// </param>
        /// <returns>
        ///     Retorna el alumno actualizado
        /// </returns>
        public async Task<StudentEntity> UpdateShoolClasses(StudentEntity studentEntity, List<SchoolClassEntity>? schoolClassEntities)
        {
            List<SchoolClassStudentEntity> schoolClassStudentEntities = new List<SchoolClassStudentEntity>();

            if (!(schoolClassEntities is null))
            {
                //Rellena la nueva lista de clases del alumno
                schoolClassEntities.ForEach(sc =>
                {
                    schoolClassStudentEntities.Add(new SchoolClassStudentEntity()
                    {
                        StudentEntity = studentEntity,
                        SchoolClassEntity = sc
                    });
                });
            }
            //Se actualiza la lista de clases del alumno
            studentEntity.SchoolClassStudentEntities = schoolClassStudentEntities;

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Clases presenciales del alumnos actualizadas en la base de datos.");

            return studentEntity;
        }

        /// <summary>
        ///     Recupera la lista de alumnos cursando el ciclo y la asignatura
        ///     introducidos por parámetro
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="subjectId"></param>
        /// <returns>
        ///     Retorna la lista de alumnos 
        /// </returns>
        public async Task<List<StudentEntity>> GetByCourseAndSubject(int courseId, int subjectId)
        {
            List<StudentEntity> studentEntities = await _dbContext.StudentEntities

                .Join(_dbContext.StudentSubjectEntities
                    .Where(ss => ss.SubjectId == subjectId),
                    s => s.Id,
                    ss => ss.StudentId,
                    (s, cs) => s)
                 .Where(s => s.CourseId == courseId)
                .ToListAsync();

            _logger.LogInformation("Lista de alumnos recuperada de la base de datos.");

            return studentEntities;
        }

        /// <summary>
        ///     Recupera la lista de los alumnos cursando el curso
        ///     pasado por parametro
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>
        ///     Retorna la lista de alumnos 
        ///</returns>
        public async Task<List<StudentEntity>> GetByCourse(int courseId)
        {
            List<StudentEntity> studentEntities = await _dbContext.StudentEntities
                .Where(s => s.CourseId == courseId)
                .ToListAsync();

            _logger.LogInformation("Lista de alumnos recuperada de la base de datos.");

            return studentEntities;
        }

        public async Task<List<StudentEntity>> GetByCourseIncludeAssignedSubjects(int courseId)
        {
            List<StudentEntity> studentEntities = await _dbContext.StudentEntities
                .Include(s => s.StudentSubjectEntities)
                .Where(s => s.CourseId == courseId)
                .ToListAsync();

            _logger.LogInformation("Lista de alumnos recuperada de la base de datos.");

            return studentEntities;
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
