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
    public class SchoolClassRepository : ISchoolClassRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<SchoolClassRepository> _logger;

        public SchoolClassRepository(IAttendanceControlDBContext dbContext, ILogger<SchoolClassRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> SetNotCurrent(int id)
        {
            
            SchoolClassEntity entity = await _dbContext.SchoolClassEntities
                .FirstOrDefaultAsync(sc => sc.Id == id);

            entity.IsCurrent = false;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public Task<SchoolClassEntity> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SchoolClassEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<SchoolClassEntity>> GetByCourse(int courseId)
        {
            return await _dbContext.SchoolClassEntities.Include(sc => sc.SubjectEntity)
                .Include(sc => sc.ScheduleEntity)
                .Where(sc => sc.CourseId == courseId && sc.IsCurrent == true).ToListAsync();
        }

        public async  Task<SchoolClassEntity> Save(SchoolClassEntity schoolClassEntity)

        {
            Console.WriteLine("\n\nAAAAAAAAAAAAAAAAAAAA" + schoolClassEntity.Id);

            try
            {
                await _dbContext.SchoolClassEntities.AddAsync(schoolClassEntity);
                await _dbContext.SaveChangesAsync();

                schoolClassEntity = await _dbContext.SchoolClassEntities
                    .Include(sc => sc.SubjectEntity)
                    .Include(sc => sc.ScheduleEntity)
                    .FirstOrDefaultAsync(sc => sc.Id == schoolClassEntity.Id);

            }
            catch(DbUpdateException ex)
            {
                Console.WriteLine("\n\nAAAAAAAAAAAAAAAAAAAA" + ex.InnerException.Message);
            }

            return schoolClassEntity;
        }

        public async Task<SchoolClassEntity> Update(SchoolClassEntity entity)
        {

            SchoolClassEntity schoolClassEntity = await _dbContext.SchoolClassEntities
                .FirstOrDefaultAsync(c => c.Id == entity.Id);

            schoolClassEntity.SubjectEntity = entity.SubjectEntity;
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
