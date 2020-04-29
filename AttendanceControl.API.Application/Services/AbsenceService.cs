using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.DTOs;
using AttendanceControl.API.Application.Mappers;
using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Services
{
    /// <summary>
    ///     Lógica relacionada a las ausencias
    /// </summary>
    public class AbsenceService : IAbsenceService
    {
        private readonly IAbsenceRepository _absenceRepository;
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IStudentRepository _studentRepository;
        private ILogger<AbsenceService> _logger;

        public AbsenceService(IAbsenceRepository absenceRepository,
                              ISchoolClassRepository schoolClassRepository,
                              IStudentRepository studentRepository,
                              ILogger<AbsenceService> logger)
        {
            _absenceRepository = absenceRepository;
            _schoolClassRepository = schoolClassRepository;
            _studentRepository = studentRepository;
            _logger = logger;
        }



        /// <summary>
        ///     Recibe del repositorio la lista de entidades ausencias  
        ///     del alumno con el id introducido por parámetro
        ///     y la transforma en lista de ausencias
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>
        ///     Retorna la lista de ausencias 
        /// </returns>
        public async Task<List<Absence>> GetByStudent(int studentId)
        {
            List<AbsenceEntity> studentEntities = await _absenceRepository
                .GetByStudent(studentId);

            List<Absence> absences = studentEntities
                .Select(s => AbsenceMapper.MapIncludingSchedule(s)).ToList();

            return absences;
        }

        public async Task<bool> Save(AbsenceDto[] createAbsenceDtos)
        {
            AbsenceEntity[] absenceEntities = new AbsenceEntity[createAbsenceDtos.Length];

            for(int i=0;i<createAbsenceDtos.Length;i++)
            {
                Console.WriteLine("EEEEEEEEEEEEE" + createAbsenceDtos[i].Type);
                SchoolClassEntity schoolClassEntity = await _schoolClassRepository.Get(createAbsenceDtos[i].SchoolClassId);
                StudentEntity studentEntity = await _studentRepository.Get(createAbsenceDtos[i].StudentId);
                AbsenceEntity absenceEntity = new AbsenceEntity()
                {
                    Type = createAbsenceDtos[i].Type,
                    Date = DateTime.Today,
                    SchoolClassEntity = schoolClassEntity,
                    StudentEntity = studentEntity
                };
                absenceEntities[i] = absenceEntity;
            }

            //bool absencesAreSaved = await _absenceRepository.Save(absenceEntities);

            return true;
        }

        /// <summary>
        ///    
        ///     
        /// </summary>
        /// <param name="absenceId"></param>
        /// <param name="isExcused"></param>
        /// <returns></returns>
        public async Task<bool> SetExcused(int absenceId, bool isExcused)
        {
            AbsenceEntity absenceEntity = await _absenceRepository.Get(absenceId);

            var result = await _absenceRepository.SetExcused(absenceEntity, isExcused);

            return result;
        }
    }
}
