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
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<SubjectRepository> _logger;

        public SubjectRepository(IAttendanceControlDBContext dbContext, ILogger<SubjectRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SubjectEntity> Get(int id)
        {
            return await _dbContext.SubjectEntities
             .Include(s => s.TeacherEntity)
             .ThenInclude(t => t.PersonDataEntity)
             .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SubjectEntity>> GetAll()
        {
            return await _dbContext.SubjectEntities
               .Include(s => s.TeacherEntity)
               .ThenInclude(t => t.PersonDataEntity)              
               .OrderBy(s => s.Name)
               .ToListAsync();
        }

       

        public async Task<SubjectEntity> Save(SubjectEntity entity)
        {
            try
            {
                await _dbContext.SubjectEntities.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("La asignatura se ha guardado correctamente");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_subject_name"))
                {
                    _logger.LogWarning("La asignatura no se ha guardado porque su nombre ya existe");
                    throw new SubjectNameDuplicateEntryException();
                }
                else
                {
                    throw ex;
                }
            }
            return entity;
        }

        public async Task<SubjectEntity> Update(SubjectEntity entity)
        {
            SubjectEntity entityToUpdate = await this.Get(entity.Id);
            entityToUpdate.Name = entity.Name;
            try
            {


                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("La asignatura se ha actualizado correctamente");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_subject_name"))
                {
                    _logger.LogWarning("La asignatura no se ha actualizado porque su nombre ya existe");
                    throw new SubjectNameDuplicateEntryException();
                }
                else
                {
                    throw ex;
                }
            }

            return entityToUpdate;
        }

        public async Task<SubjectEntity> UpdateAssignedTeacher(int subjectId, int? teacherId)
        {
            
            SubjectEntity subjectEntity = await this.Get(subjectId);
            if(teacherId is null)
            {
                subjectEntity.TeacherEntity = null;
            }
            else
            {
                TeacherEntity teacherEntity = await _dbContext.TeacherEntities.Include(t => t.PersonDataEntity)
                .FirstOrDefaultAsync(t => t.Id == teacherId);

                subjectEntity.TeacherEntity = teacherEntity;
            }
            
            
            await _dbContext.SaveChangesAsync();

            return subjectEntity;
        }
    }
}
