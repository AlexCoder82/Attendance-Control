using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.Mappers;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Services
{
    /// <summary>
    ///     Lógica relacionada a los ciclos formativos
    /// </summary>
    public class CycleService : ICycleService
    {
        private readonly ICycleRepository _cycleRepository;
        private readonly ILogger<CycleService> _logger;

        public CycleService(ICycleRepository cycleRepository,
                            ILogger<CycleService> logger)
        {
            _cycleRepository = cycleRepository;
            _logger = logger;
        }

        /// <summary>
        ///     Lista todos los ciclos formativos disponibles
        ///     con sus cursos y horarios
        /// </summary>
        /// <returns>
        ///     Retorna una lista de objetos Cycle que contienen
        ///     cada uno un objeto Shift el cual contiene un objeto
        ///     Schedules
        /// </returns>
        public async Task<List<Cycle>> GetAll()
        {

            List<CycleEntity> cycleEntities = await _cycleRepository.GetAllIncludingCoursesSubjectsAndSchedules();

            List<Cycle> cycles = cycleEntities
                .Select(c => CycleMapper.MapIncludingCoursesAndSchedules(c))
                .ToList();
          
            return cycles;

        }

        /// <summary>
        ///     Crea un nuevo ciclo formativo
        /// </summary>
        /// <param name="cycle">
        ///     EL objeto Cycle que contiene los datos sobre el ciclo
        /// </param>
        /// <exception cref="GradeNameDuplicateEntryException">
        ///     Lanza GradeNameDuplicateEntryException
        /// </exception>
        /// <returns>
        ///     Retorna el objeto Cycle guardado con su id generado
        /// </returns>
        public async Task<Cycle> Save(Cycle cycle)//Throw GradeNameDuplicateEntryException
        {
     
            CycleEntity cycleEntity = CycleMapper.Map(cycle);

            cycleEntity = await _cycleRepository.Save(cycleEntity);

            cycleEntity = await _cycleRepository
                .GetIncludingCoursesAndAssignedSubjects(cycleEntity.Id);
 
            cycle = CycleMapper.MapIncludingCourses(cycleEntity);

            return cycle;

        }

        /// <summary>
        ///     Actualiza un ciclo
        /// </summary>
        /// <param name="cycle">
        ///     El objeto Cycle con los nuevos datos del ciclo
        /// </param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        public async Task<bool> Update(Cycle cycle)
        {

            CycleEntity cycleEntity = CycleMapper.Map(cycle);

            bool result = await _cycleRepository.Update(cycleEntity);

            return result;

        }

    }
}
