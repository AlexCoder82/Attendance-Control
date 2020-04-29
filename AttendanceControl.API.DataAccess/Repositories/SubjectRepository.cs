using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    /// <summary>
    ///     Clase con los métodos de acceso a la tabla de las asignaturas
    /// </summary>
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<SubjectRepository> _logger;

        public SubjectRepository(IAttendanceControlDBContext dbContext,
                                 ILogger<SubjectRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        ///     Recupera la asignatura con el id introducido en parámetro
        ///     incluyendo el profesor asignado y sus datos personales  
        /// </summary>
        /// <param name="id">
        ///     El id de la asignatura
        /// </param>
        /// <returns>
        ///     Retorna la entidad asignatura o lanza DataNotFoundException 
        ///     si el id no existe
        /// </returns>
        public async Task<SubjectEntity> GetIncludingAssignedTeacher(int subjectId)
        {
            SubjectEntity subjectEntity = await _dbContext.SubjectEntities
                 .Include(s => s.TeacherEntity)
                 .FirstOrDefaultAsync(s => s.Id == subjectId);

            if(subjectEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado la asignatura, " +
                    "el id no existe");
            }

            _logger.LogInformation("Asignatura recuperada de la base de datos");

            return subjectEntity;
        }

        /// <summary>
        ///     Recupera la lista completa de asignaturas
        ///     incluyendo los profesores asignados
        /// </summary>
        /// <returns>
        ///     Retorna la lista de entidades asignaturas
        /// </returns>
        public async Task<List<SubjectEntity>> GetAllIncludingAssignedTeacher()
        {
            List<SubjectEntity> subjectEntities = await _dbContext.SubjectEntities
               .Include(s => s.TeacherEntity)
               .OrderBy(s => s.Name)
               .ToListAsync();

            _logger.LogInformation("Lista de asignaturas recuperada de la base de datos");

            return subjectEntities;
        }


        /// <summary>
        ///     Guarda una nueva asignatura
        /// </summary>
        /// <param name="subjectEntity">
        ///     La entidad asignatura a guardar
        /// </param>
        /// <returns>
        ///     Retorna la asignatura guardada con su id
        ///     generado por la base de datos o lanza
        ///     SubjectNameDuplicateEntryException si
        ///     el nombre de la asignatura ya existe en la tabla
        /// </returns>
        public async Task<SubjectEntity> Save(SubjectEntity subjectEntity)
        {
            try
            {
                await _dbContext.SubjectEntities.AddAsync(subjectEntity);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Asignatura guardada correctamente");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_subject_name"))
                {
                    _logger.LogWarning("La asignatura no se ha guardado porque " +
                        "su nombre ya existe");
                    
                    throw new SubjectNameDuplicateEntryException();
                }
                //Cualquier otra razón, lanzo la excepción
                else
                {
                    throw ex;
                }
            }

            return subjectEntity;
        }

        /// <summary>
        ///     Actualiza el nombre de una asignatura
        /// </summary>
        /// <param name="subjectEntity">
        ///     La entidad asignatura con el nuevo nombre
        /// </param>
        /// <returns>
        ///     Retorna la entidad asignatura modificada o lanza
        ///     SubjectNameDuplicateEntryException si el nombre
        ///     ya existe
        /// </returns>
        public async Task<SubjectEntity> Update(SubjectEntity subjectEntity)
        {
            //Recupera la asignatura a modificar 
            SubjectEntity entityToUpdate = await this.Get(subjectEntity.Id);

            entityToUpdate.Name = subjectEntity.Name;

            try
            {
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Asignatura actualizada correctamente");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_subject_name"))
                {
                    _logger.LogWarning("La asignatura no se ha actualizado " +
                        "porque su nombre ya existe");

                    throw new SubjectNameDuplicateEntryException();
                }
                //Cualquier otra razón, lanzo la excepción

                else
                {
                    throw ex;
                }
            }

            return entityToUpdate;
        }

        /// <summary>
        ///     Actualiza el profesor asignado a una asignatura
        /// </summary>
        /// <param name="subjectId">
        ///     El id de la asignatura
        /// </param>
        /// <param name="teacherId">
        ///     El id del profesor(Opcional)
        /// </param>
        /// <returns></returns>
        public async Task<SubjectEntity> UpdateAssignedTeacher(int subjectId, TeacherEntity? teacherEntity)
        {
            //Recupera la asignatura a modificar
            SubjectEntity subjectEntity = await this
                .GetIncludingAssignedTeacher(subjectId);

            //Si la entidad profesor es nula, la asignatura queda sin profesor
            //asignado
            if (teacherEntity is null)
            {
                subjectEntity.TeacherId = null;
            }
            //Sino se asigna el nuevo profesor
            else
            {
                subjectEntity.TeacherEntity = teacherEntity;
            }


            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Profesor de la asignatura asignado correctamente");

            return subjectEntity;
        }

        /// <summary>
        ///        Recupera la asignatura con el id introducido en parámetro
        /// </summary>
        /// <param name="subjectId">
        ///     El id de la entidad asignatura
        /// </param>
        /// <returns>
        ///     Retorna la entidad asignatura o lanza DataNotFoundException
        ///     si no existe el id en la tabla
        /// </returns>
        public async Task<SubjectEntity> Get(int subjectId)
        {
            SubjectEntity subjectEntity = await _dbContext.SubjectEntities
                .FirstOrDefaultAsync(s => s.Id == subjectId);

            if (subjectEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado la asignatura, " +
                    "el id no existe");
            }

            _logger.LogInformation("Asignatura recuperada de la base de datos");

            return subjectEntity;
        }

        /// <summary>
        ///     Recupera la lista de todas las asignaturas dado un curso
        ///     introducido por parámetro
        /// </summary>
        /// <param name="courseId">
        ///     El id del curso
        /// </param>
        /// <returns>
        ///     Retorna la lista de asignaturas del curso
        /// </returns>
        public async Task<List<SubjectEntity>> GetByCourse(int courseId)
        {
            List<SubjectEntity> subjectEntities = await _dbContext.SubjectEntities
                .Join(_dbContext.CourseSubjectEntities
                    .Where(cs => cs.CourseId == courseId),
                    s => s.Id,
                    cs => cs.SubjectId,
                    (s, cs) => s)
                .ToListAsync();

            _logger.LogInformation("Lista de asignaturas del curso recuperada de la base de datos");

            return subjectEntities;
        }
    }
}
