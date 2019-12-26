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
    public class CourseRepository : ICourseRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(IAttendanceControlDBContext dbContext, ILogger<CourseRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CourseEntity> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CourseEntity> Save(CourseEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseEntity > UpdateSchoolClasses(CourseEntity courseEntity)
        {
            var entity = await _dbContext.CourseEntities
                    .Include(c => c.SchoolClassEntities)
                    .FirstOrDefaultAsync(c => c.Id == courseEntity.Id);

            entity.SchoolClassEntities = courseEntity.SchoolClassEntities;

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<CourseEntity> Update(CourseEntity entity)
        {

            var entityToUpdate = await _dbContext.CourseEntities
                    .Include(c => c.CourseSubjectEntities)
                    .FirstOrDefaultAsync(c => c.Id == entity.Id);

            entityToUpdate.CourseSubjectEntities = entity.CourseSubjectEntities;

            //await _dbContext.SaveChangesAsync();
            try
            {
              //  _dbContext.CourseEntities.Update(entity);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("El courso se ha actualizado correctamente");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_course_subject"))
                {
                    _logger.LogWarning("El courso no se ha actualizado porque " +
                        "tiene varias veces la misma asignatura");
                    throw new CourseSubjectDuplicateEntryException();
                }
                else
                {
                    throw ex;
                }
            }
            return entity;
        }
    }
}
