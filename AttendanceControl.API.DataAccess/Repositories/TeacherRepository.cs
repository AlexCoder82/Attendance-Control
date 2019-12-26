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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<TeacherRepository> _logger;

        public TeacherRepository(IAttendanceControlDBContext dbContext, ILogger<TeacherRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TeacherEntity> Get(int id)
        {
            return await _dbContext.TeacherEntities
               .Include(t => t.PersonDataEntity)
               .Where(t => t.Id == id)
               .Select(t => new TeacherEntity()
               {
                   Id = t.Id,
                   PersonDataEntity = new PersonDataEntity()
                   {
                       Dni = t.PersonDataEntity.Dni,
                       FirstName = t.PersonDataEntity.FirstName,
                       LastName1 = t.PersonDataEntity.LastName1,
                       LastName2 = t.PersonDataEntity.LastName2
                   }
               })
               .FirstOrDefaultAsync();
        }

        public async Task<List<TeacherEntity>> GetAll()
        {
            return await _dbContext.TeacherEntities
                .Include(t => t.PersonDataEntity)
                .Select(t => new TeacherEntity()
                {
                    Id = t.Id,
                    PersonDataEntity = new PersonDataEntity()
                    {
                        Dni = t.PersonDataEntity.Dni,
                        FirstName = t.PersonDataEntity.FirstName,
                        LastName1 = t.PersonDataEntity.LastName1,
                        LastName2 = t.PersonDataEntity.LastName2
                    }
                })
                .ToListAsync();
        }

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
                    _logger.LogWarning("El profesor no se ha actualizado porque su dni ya existe");
                    throw new DniDuplicateEntryException();
                }
                else
                {
                    throw ex;
                }
            }

            return entity;
        }

        public async Task<TeacherEntity> Update(TeacherEntity entity)
        {
            TeacherEntity entityToUpdate = await _dbContext.TeacherEntities
                .Include(t => t.PersonDataEntity)
                .FirstOrDefaultAsync(t => t.Id == entity.Id);

            entityToUpdate.PersonDataEntity.Dni = entity.PersonDataEntity.Dni;
            entityToUpdate.PersonDataEntity.FirstName = entity.PersonDataEntity.FirstName;
            entityToUpdate.PersonDataEntity.LastName1 = entity.PersonDataEntity.LastName1;
            entityToUpdate.PersonDataEntity.LastName2 = entity.PersonDataEntity.LastName2;
            try
            {
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("El profesor se ha actualizado correctamente");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_dni"))
                {
                    _logger.LogWarning("La asignatura no se ha actualizado porque su nombre ya existe");
                    throw new DniDuplicateEntryException();
                }
                else
                {
                    throw ex;
                }
            }

            return entityToUpdate;
        }
    }
}
