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
    ///     Clase con los métodos de acceso a la tabla de profesores
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

        public async Task<TeacherEntity> GetByCredentials(string username, string password)
        {
            TeacherEntity teacherEntity = await _dbContext.TeacherEntities
                .Include(t => t.TeacherCredentialsEntity)
                .FirstOrDefaultAsync(t => t.TeacherCredentialsEntity.Username == username
                    && t.TeacherCredentialsEntity.Password == password);

            return teacherEntity;
        }

        /// <summary>
        ///     Recupera el profesor que corresponde al id introducido 
        ///     en parámetro
        /// </summary>
        /// <param name="id">
        ///     El id del profesor
        /// </param>
        /// <returns>
        ///     Retorna la entidad profesor con sus datos personales
        ///     pero sin sus credenciales delogin
        /// </returns>
        public async Task<TeacherEntity> Get(int id)
        {
            TeacherEntity teacherEntity = await _dbContext.TeacherEntities
               .Where(t => t.Id == id)
               //.Select(t => new TeacherEntity()
               //{
               //    Id = t.Id,
               //    PersonDataEntity = new PersonDataEntity()
               //    {
               //        Dni = t.PersonDataEntity.Dni,
               //        FirstName = t.PersonDataEntity.FirstName,
               //        LastName1 = t.PersonDataEntity.LastName1,
               //        LastName2 = t.PersonDataEntity.LastName2
               //    }
               //})
               .FirstOrDefaultAsync();

            if (teacherEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado ningún profesor, no existe el id");
            }

            _logger.LogInformation("Profesor recuperado de la base de datos");

            return teacherEntity;
        }

        /// <summary>
        ///     Recupera la lista completa de los profesores
        /// </summary>
        /// <returns>
        ///     Retorna una lista de entidades de profesores
        ///     con sus datos personales pero sin los credenciales de login
        /// </returns>
        public async Task<List<TeacherEntity>> GetAll()
        {
            List<TeacherEntity> teacherEntities = await _dbContext.TeacherEntities.OrderBy(t=>t.LastName1)     
                .ToListAsync();

            _logger.LogInformation("Lista de profesores recuperada de la base de datos");

            return teacherEntities;
        }
      

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

        public async Task<bool> Register(TeacherEntity teacherEntity, TeacherCredentialsEntity teacherCredentialsEntity)
        {
            try
            {
                teacherEntity.TeacherCredentialsEntity = teacherCredentialsEntity;
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_username"))
                {
                    throw new TeacherUserNameDuplicateEntryException();
                }
                if (ex.InnerException.Message.Contains("UQ_password"))
                {
                    throw new TeacherPasswordDuplicateEntryException();
                }
            }

            return true;
        }

        /// <summary>
        ///     Guarda un nuevo profesor
        /// </summary>
        /// <param name="entity">
        ///     La entidad profesor a guardar
        /// </param>
        /// <returns>
        ///     Retorna la entidad profesor guardada 
        ///     con su Id generado por la base de datos o
        ///     lanza DniDuplicateEntryException si el dni
        ///     ya existe en la tabla
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
        ///     Actualiza los datos personales de un profesor
        /// </summary>
        /// <param name="entity">
        ///     La entidad profesor con los nuevos datos
        /// </param>
        /// <returns>
        ///     Retorna la entidad profesor o lanza 
        ///     DniDuplicateEntryException si 
        ///     el dni ya existe en la tabla
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
