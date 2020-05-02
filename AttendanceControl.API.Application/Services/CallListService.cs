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
    ///     Lógica relacionada con los listados de alumnos 
    /// </summary>
    public class CallListService : ICallListService
    {
        private readonly IAbsenceRepository _absenceRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<CallListService> _logger;

        public CallListService(IStudentRepository studentRepository,
                               IAbsenceRepository absenceRepository,
                               ILogger<CallListService> logger)
        {
            _studentRepository = studentRepository;
            _absenceRepository = absenceRepository;
            _logger = logger;
        }

        /// <summary>
        ///     Crea la lista de los alumnos que deben asistir a 
        ///     clases.
        /// </summary>
        /// <param name="schoolClassIds">
        ///     array de ids de las clases.
        /// </param>
        /// <returns>
        ///     Retorna una lista de objetos SchoolClassStudent que pueden 
        ///     contener objetos Absence en caso de que no sea la primera 
        ///     vez que el profesor hace la peticion 
        /// </returns>
        public async Task<List<SchoolClassStudent>> Get(int[] schoolClassIds)
        {

            List<SchoolClassStudent> callList = new List<SchoolClassStudent>();

            foreach (int id in schoolClassIds)
            {
                
                //Alumnos que deben ir a cada clase
                List<StudentEntity> studentEntities = await _studentRepository
                    .GetByCurrentSchoolClass(id);
                _logger.LogInformation("AAAAAAAAAAAAAAA" + studentEntities.Count);
                foreach (StudentEntity s in studentEntities)
                {
                    //Para cada alumno de la lista, se mira si ya ha sido marcado como
                    //ausente, en tal caso se instancia un objeto Absence dentro del objeto
                    //SchoolClassStudent para que el profesor no tenga que volver a marcar 
                    //a los ausentes cuando recibe la lista

                    AbsenceEntity absenceEntity = await _absenceRepository
                        .GetByStudentAndSchoolClass(s.Id, id);

                    Absence absence = AbsenceMapper.Map(absenceEntity);

                    callList.Add(new SchoolClassStudent
                    {
                        SchoolClassID = id,
                        Absence = absence,
                        Student = StudentMapper.Map(s)
                    });
                }
            }

            ;
            return callList;

        }

        /// <summary>
        ///     Recibe la lista de alumnos ausentes y 
        ///     envia una lista de objetos Absence al repositorio
        /// </summary>
        /// <param name="callList">
        ///     La lista de alumnos ausentes
        /// </param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        public async Task<bool> Post(List<SchoolClassStudent> callList)
        {

            //Obtengo la lista de entidades Absence a partir del listado de alumnos
            List<AbsenceEntity> absenceEntities = callList
                .Where(cl => !(cl.Absence is null))
                .Select(cl => new AbsenceEntity
                {
                    SchoolClassId = cl.SchoolClassID,
                    StudentId = cl.Student.Id,
                    Type = cl.Absence.Type
                })
                .ToList();

            await _absenceRepository.Save(absenceEntities);

            return true;

        }

    }
}
