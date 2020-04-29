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
    public class CallListService : ICallListService
    {
        private readonly IDatabaseTransaction _databaseTransaction;
        private readonly IAbsenceRepository _absenceRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<CallListService> _logger;

        public CallListService(IDatabaseTransaction databaseTransaction,
            IStudentRepository studentRepository,
            IAbsenceRepository absenceRepository,
                                  ILogger<CallListService> logger)
        {
            _databaseTransaction = databaseTransaction;
            _studentRepository = studentRepository;
            _absenceRepository = absenceRepository;
            _logger = logger;
        }

        public async Task<List<SchoolClassStudent>> Get(int[] schoolClassIds)
        {
            List<SchoolClassStudent> callList = new List<SchoolClassStudent>();
            foreach (int id in schoolClassIds)
            {

                List<StudentEntity> studentEntities = await _studentRepository.GetByCurrentSchoolClass(id);

                foreach (StudentEntity s in studentEntities)
                {
                    Student student = StudentMapper.Map(s);

                    AbsenceEntity absenceEntity = await _absenceRepository.GetByStudentAndSchoolClass(s.Id, id);

                    Absence absence = AbsenceMapper.Map(absenceEntity);
                    callList.Add(new SchoolClassStudent
                    {
                        SchoolClassID = id,
                        Absence = absence,
                        Student = student
                    });
                }
            }

            return callList;
        }

        public async Task<bool> Post(List<SchoolClassStudent> callList)
        {
            //Obtengo la lista de entidades ausencias a partir del listado de alumnos
            List<AbsenceEntity> absenceEntities = callList.
                Where(cl=>!(cl.Absence is null))
                .Select(cl=> new AbsenceEntity
                {
                    SchoolClassId = cl.SchoolClassID,
                    StudentId = cl.Student.Id,
                    Type = cl.Absence.Type
                }).ToList();
              

            await _absenceRepository.Save(absenceEntities);
              
            
            return true;
        }
    }
}
