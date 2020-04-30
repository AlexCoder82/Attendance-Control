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
    /// <summary>
    ///     Lógica relacionada con las clases
    /// </summary>
    public class SchoolClassService : ISchoolClassService
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<SchoolClassService> _logger;

        public SchoolClassService(ISchoolClassRepository schoolClassRepository,
                                  IStudentRepository studentRepository,
                                  ILogger<SchoolClassService> logger)
        {
            _schoolClassRepository = schoolClassRepository;
            _studentRepository = studentRepository;
            _logger = logger;
        }

        /// <summary>
        ///     Cancela una clase
        /// </summary>
        /// <param name="schoolClassId">
        ///     El id de la clase
        /// </param>
        /// <returns>
        ///     Retorna true;
        /// </returns>
        public async Task<bool> Cancel(int schoolClassId)
        {

            await _schoolClassRepository.Cancel(schoolClassId);

            return true;

        }

        /// <summary>
        ///     Crea una nueva clase
        /// </summary>
        /// <param name="schoolClass">
        ///     El objeto SchoolClass que contiene los datos de la clase
        /// </param>
        /// <returns>
        ///     Retorna el mismo objeto SchooClass con el id generado
        /// </returns>
        public async Task<SchoolClass> Save(SchoolClass schoolClass)
        {

            SchoolClassEntity schoolClassEntity = SchoolClassMapper.Map(schoolClass);

            schoolClassEntity = await _schoolClassRepository.Save(schoolClassEntity);

            //Actualizo el id con el id retornado por el repositorio
            schoolClass.Id = schoolClassEntity.Id;

            return schoolClass;

        }

        /// <summary>
        ///     Lista todas las clases de un curso
        /// </summary>
        /// <param name="courseId">
        ///     El id del curso
        /// </param>
        /// <returns>
        ///     Retorna una lista de objetos SchoolClass
        /// </returns>
        public async Task<List<SchoolClass>> GetByCourse(int courseId)
        {
            List<SchoolClassEntity> schoolClassEntities = await _schoolClassRepository.GetByCourse(courseId);

            List<SchoolClass> schoolClasses = schoolClassEntities
                .Select(sc => SchoolClassMapper.Map(sc)).ToList();

            return schoolClasses;
        }

        /// <summary>
        ///     Retorna la lista de clases con su asignatura y horarios 
        ///     que un profesor imparte "hoy"
        /// </summary>
        /// <param name="teacherId">
        ///     El id del profesor 
        /// </param>
        /// <returns>
        ///     Retorna una lista de objetos SchoolClass que contienen 
        ///     cada uno un objeto Subject y un objeto Schedules
        /// </returns>
        public async Task<List<SchoolClass>> GetByTeacher(int teacherId)
        {

            List<SchoolClassEntity> schoolClassEntities = await _schoolClassRepository
                .GetByTeacher(teacherId);

            List<SchoolClass> schoolClasses = schoolClassEntities
                 .Select(sc => SchoolClassMapper
                        .Map(sc)).ToList();

            return schoolClasses;

        }

    }
}
