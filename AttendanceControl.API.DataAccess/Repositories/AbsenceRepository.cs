using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    /// <summary>
    ///  
    /// </summary>
    public class AbsenceRepository : IAbsenceRepository
    {

        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<AbsenceRepository> _logger;

        public AbsenceRepository(IAttendanceControlDBContext dbContext,
                                 ILogger<AbsenceRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> DeleteByTodayDateAndSchoolClass(int schoolClassId)
        {
            List<AbsenceEntity> absenceEntities = await _dbContext.AbsenceEntities
                .Where(a => a.Date == DateTime.Today && a.SchoolClassId == schoolClassId)
                .ToListAsync();

            if (absenceEntities.Count > 0)
            {
                _logger.LogInformation("AAAAAAAAAAAAAAAAAAAAAA");

                _dbContext.AbsenceEntities.RemoveRange(absenceEntities);
                await _dbContext.SaveChangesAsync();

            }

            return true;

        }

        public async Task<AbsenceEntity> Get(int absenceId)
        {
            AbsenceEntity absenceEntity = await _dbContext.AbsenceEntities.FirstOrDefaultAsync(a => a.Id == absenceId);

            if (absenceEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado la ausencia, el id no existe");
            }

            return absenceEntity;
        }

        public async Task<List<AbsenceEntity>> GetByStudent(int studentId)
        {
            return await _dbContext.AbsenceEntities
                .Include(a => a.SchoolClassEntity).ThenInclude(sc => sc.ScheduleEntity)
                .Include(a => a.SchoolClassEntity)
                    .ThenInclude(sc => sc.SubjectEntity)
                .Where(a => a.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<AbsenceEntity> GetByStudentAndSchoolClass(int studentId, int schoolClassId)
        {
            return await _dbContext.AbsenceEntities
                .FirstOrDefaultAsync(a => a.StudentId == studentId && a.Date == DateTime.Today);
        }

        public async Task<bool> Save(List<AbsenceEntity> absenceEntities)
        {

            absenceEntities.ForEach(a =>
           {
               var schoolClassId = new MySqlParameter("@schoolClassId", a.SchoolClassId);
               var studentId = new MySqlParameter("@studentId", a.StudentId);
               var type = new MySqlParameter("@type", a.Type);

               _dbContext.Database.ExecuteSqlRaw("call insert_absence(@schoolClassId,@studentId,@type)", schoolClassId, studentId, type);

           });

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetExcused(AbsenceEntity absenceEntity, bool isExcused)
        {
            absenceEntity.IsExcused = isExcused;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
