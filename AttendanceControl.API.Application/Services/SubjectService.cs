using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.Mappers;
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
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ILogger<SubjectService> _logger;

        public SubjectService(ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository,
            ILogger<SubjectService> logger)
        {
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
            _logger = logger;
        }

        public Task<Subject> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Subject>> GetAll()
        {
            List<SubjectEntity> subjectEntities = await _subjectRepository.GetAll();

             Console.WriteLine("\n\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA " + "\n\n");
            Console.WriteLine(subjectEntities.Count);

            List<Subject> subjects = subjectEntities
                .Select(s => SubjectMapper.MapIncludingTeacher(s))
                .ToList();

            return subjects;
        }

        public async Task<Subject> Save(Subject subject)
        {
            SubjectEntity subjectEntity = SubjectMapper.Map(subject);
            subjectEntity = await _subjectRepository.Save(subjectEntity);

            //cycleEntity = await _cycleRepository.Get(cycleEntity.Id);
            // Console.WriteLine("\n\nAAAAAAAAAAAAAAAAAAAAAAAAAA" +cycleEntity.Id);
            subject = SubjectMapper.Map(subjectEntity);

            return subject;
        }

        public async Task<Subject> Update(Subject subject)
        {
            SubjectEntity subjectEntity = SubjectMapper.Map(subject);
            subjectEntity = await _subjectRepository.Update(subjectEntity);

            //cycleEntity = await _cycleRepository.Get(cycleEntity.Id);
            // Console.WriteLine("\n\nAAAAAAAAAAAAAAAAAAAAAAAAAA" +cycleEntity.Id);
            subject = SubjectMapper.Map(subjectEntity);

            return subject;
        }

        public async Task<Subject> UpdateAssignedTeacher(int subjectId, int? teacherId)
        {
          
            SubjectEntity subjectEntity = await _subjectRepository.UpdateAssignedTeacher(subjectId,teacherId);

            Subject subject = SubjectMapper.MapIncludingTeacher(subjectEntity);

            return subject;
        }
    }
}
