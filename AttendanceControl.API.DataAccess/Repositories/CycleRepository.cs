﻿using AttendanceControl.API.Business.Exceptions;
using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Repositories
{
    public class CycleRepository : IRepository<CycleEntity>, ICycleRepository
    {
        private readonly IAttendanceControlDBContext _dbContext;
        private readonly ILogger<CycleRepository> _logger;

        public CycleRepository(IAttendanceControlDBContext dbContext, ILogger<CycleRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> Delete(int id)
        {
            var cycleEntity = await _dbContext.CycleEntities.FindAsync(id);

            if (cycleEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado el ciclo a borrar en la base de datos.");
            }

            _dbContext.CycleEntities.Remove(cycleEntity);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("El ciclo ha sido borrado correctamente");

            return true;
        }

        public async Task<CycleEntity> Get(int id)
        {
            var cycleEntity = await _dbContext.CycleEntities
                .Include(c => c.CourseEntities)
                .ThenInclude(co => co.CourseSubjectEntities)
                .ThenInclude(cs => cs.SubjectEntity).FirstOrDefaultAsync(c => c.Id == id);

            if (cycleEntity is null)
            {
                throw new DataNotFoundException("No se ha encontrado el ciclo en la base de datos.");
            }

            return cycleEntity;
        }

        /// <summary>
        ///     Lista todos los grados de la base de datos
        /// </summary>
        /// <returns>
        ///     Retorna una lista de los grados ordenados por nombre 
        ///     incluyendo la lista de asignaturas
        ///     de cada uno de los ciclos de cada grado
        /// </returns>
        public async Task<List<CycleEntity>> GetAll()
        {
            return await _dbContext.CycleEntities
                .Include(c => c.CourseEntities)
                .ThenInclude(co => co.CourseSubjectEntities)
                .ThenInclude(cs => cs.SubjectEntity)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<CycleEntity> Save(CycleEntity cycleEntity)
        {
            try
            {
                await _dbContext.CycleEntities.AddAsync(cycleEntity);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("El ciclo se ha guardado correctamente");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_cycle_name"))
                {
                    _logger.LogWarning("El ciclo no se ha guardado porque su nombre ya existe");
                    throw new GradeNameDuplicateEntryException();
                }
                else
                {
                    throw ex;
                }
            }
            return cycleEntity;
        }

        public async Task<CycleEntity> Update(CycleEntity cycleEntity)
        {
            try
            {
                _dbContext.CycleEntities.Update(cycleEntity);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("El ciclo se ha actualizado correctamente");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Message.Contains("UQ_cycle_name"))
                {
                    _logger.LogWarning("El ciclo no se ha actualizado porque su nombre ya existe");
                    throw new GradeNameDuplicateEntryException();
                }
                else
                {
                    throw ex;
                }
            }
            return cycleEntity;
        }
    }
}
