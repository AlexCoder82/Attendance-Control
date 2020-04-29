using AttendanceControl.API.Application.Contracts.DTOs;
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
    public class SchoolClassService : ISchoolClassService
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IDatabaseTransaction _databaseTransaction;
        private readonly ILogger<SchoolClassService> _logger;

        public SchoolClassService(ISchoolClassRepository schoolClassRepository,
                                  IStudentRepository studentRepository,
                                  IDatabaseTransaction databaseTransaction,
                                  ILogger<SchoolClassService> logger)
        {
            _schoolClassRepository = schoolClassRepository;
            _studentRepository = studentRepository;
            _databaseTransaction = databaseTransaction;
            _logger = logger;
        }

        public async Task<bool> Cancel(int schoolClassId)
        {
    
                await _schoolClassRepository.Cancel(schoolClassId);


                return true;
          
        }

        public async Task<SchoolClass> Save(SchoolClass schoolClass)
        {

            SchoolClassEntity schoolClassEntity = SchoolClassMapper.Map(schoolClass);

            schoolClassEntity = await _schoolClassRepository.Save(schoolClassEntity);

            //Actualizo el id con el id retornado por el repositorio
            schoolClass.Id = schoolClassEntity.Id;

            return schoolClass;

        }

        public async Task<List<SchoolClass>> GetByCourse(int courseId)
        {
            List<SchoolClassEntity> schoolClassEntities = await _schoolClassRepository.GetByCourse(courseId);

            List<SchoolClass> schoolClasses = schoolClassEntities
                .Select(sc => SchoolClassMapper.Map(sc)).ToList();

            return schoolClasses;
        }

        public async Task<List<SchoolClass>> GetByTeacher(int teacherId)
        {
            List<SchoolClassEntity> schoolClassEntities = await _schoolClassRepository
                .GetByTeacher(teacherId);
            List<SchoolClass> schoolClasses = schoolClassEntities
                 .Select(sc => SchoolClassMapper.MapIncludingSubjectScheduleAndStudents(sc)).ToList();

            return schoolClasses;
        }

    }
}
