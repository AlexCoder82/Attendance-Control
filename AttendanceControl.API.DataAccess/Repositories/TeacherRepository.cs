using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    /// <summary>
    ///     Repositorio de profesores
    /// </summary>
    public class TeacherRepository : ITeacherRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<TeacherRepository> _logger;

        public TeacherRepository(IAttendanceControlDBContext dbContext,
                                 ILogger<TeacherRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        ///     Recupera una entidad profesor por su id
        /// </summary>
        /// <param name="id">
        ///     El id del profesor
        /// </param>
        /// <exception cref="DataNotFoundException">
        ///     Lanza DataNotFoundException si el id no existe
        /// </exception>
        /// <returns>
        ///     Retorna la entidad profesor 
        /// </returns>
        public async Task<TeacherEntity> Get(int id)
        {

            TeacherEntity teacherEntity = await _dbContext.TeacherEntities
               .Where(t => t.Id == id)            
               .FirstOrDefaultAsync();

            if (teacherEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado ningún profesor, no existe el id");
            }

            _logger.LogInformation("Profesor recuperado de la base de datos");

            return teacherEntity;

        }

        /// <summary>
        ///     Recupera la lista completa de todas las entidades profesor
        /// </summary>
        /// <returns>
        ///     Retorna una lista de entidades de profesores
        /// </returns>
        public async Task<List<TeacherEntity>> GetAll()
        {

            List<TeacherEntity> teacherEntities = await _dbContext.TeacherEntities.OrderBy(t=>t.LastName1)     
                .ToListAsync();

            _logger.LogInformation("Lista de profesores recuperada de la base de datos");

            return teacherEntities;

        }

        /// <summary>
        ///     Comprueba si una entidad profesor existe 
        ///     por su dni
        /// </summary>
        /// <param name="dni"></param>
        /// <exception cref="WrongDniException">
        ///     Lanza WrongDniException si no existe
        /// </exception>
        /// <returns>
        ///     Retorna la entidad profesor
        /// </returns>

        public async Task<TeacherEntity> GetByDni(string dni)
        {

            TeacherEntity teacherEntity = await _dbContext.TeacherEntities
               .Where(t => t.Dni == dni)        
               .FirstOrDefaultAsync();

            if (teacherEntity is null)
            {
                throw new WrongDniException();
            }       

            _logger.LogInformation("Profesor con dni " +dni +"reconocido");

            return teacherEntity;

        }



        /// <summary>
        ///     Inserta una nueva entidad profesor
        /// </summary>
        /// <param name="entity">
        ///     La entidad profesor a guardar
        /// </param>
        /// <exception cref="DniDuplicateEntryException">
        ///     Lanza DniDuplicateEntryException si ya existe
        ///     un profesor con el mismo dni
        /// </exception>
        /// <returns>
        ///     Retorna la entidad profesor guardada 
        ///     con su Id generado 
        /// </returns>
        public async Task<TeacherEntity> Save(TeacherEntity entity)
        {

            try
            {
                await _dbContext.TeacherEntities.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("El profesor se ha guardado correctamente");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_dni"))
                {
                    _logger.LogWarning("El profesor no se ha actualizado " +
                        "porque su dni ya existe en la base de datos");
                    
                    throw new DniDuplicateEntryException();
                }
                //Por cualquier otra razon lanzo la excepción
                else
                {
                    throw ex;
                }
            }

            return entity;

        }

        /// <summary>
        ///     Actualiza una entidad profesor
        /// </summary>
        /// <param name="entity">
        ///     La entidad profesor con los nuevos datos
        /// </param>
        /// <exception cref="DniDuplicateEntryException">
        ///     Lanza DniDuplicateEntryException si ya existe
        ///     un profesor con el mismo dni
        /// </exception>
        /// <returns>
        ///     Retorna la entidad profesor actualizada
        /// </returns>
        public async Task<TeacherEntity> Update(TeacherEntity entity)
        {

            //Recupera la entidad a modificar
            TeacherEntity entityToUpdate = await this.Get(entity.Id);

            entityToUpdate.Dni = entity.Dni;
            entityToUpdate.FirstName = entity.FirstName;
            entityToUpdate.LastName1 = entity.LastName1;
            entityToUpdate.LastName2 = entity.LastName2;

            try
            {
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Profesor actualizado correctamente");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_dni"))
                {
                    _logger.LogWarning("La asignatura no se ha actualizado porque su nombre ya existe");
                   
                    throw new DniDuplicateEntryException();
                }
                //Por cualquier otra razon lanzo la excepción
                else
                {
                    throw ex;
                }
            }

            return entityToUpdate;

        }

    }
}
