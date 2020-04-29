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
        private readonly ITeacherRepository _teachertRepository;
        private readonly ILogger<SubjectService> _logger;

        public SubjectService(ISubjectRepository subjectRepository,
                              ITeacherRepository teacherRepository,
                              ILogger<SubjectService> logger)
        {
            _subjectRepository = subjectRepository;
            _teachertRepository = teacherRepository;
            _logger = logger;
        }


        public async Task<List<Subject>> GetAllIncludingAssignedTeacher()
        {
            List<SubjectEntity> subjectEntities = await _subjectRepository
                .GetAllIncludingAssignedTeacher();

            List<Subject> subjects = subjectEntities
                .Select(s => SubjectMapper.MapIncludingTeacher(s))
                .ToList();

            return subjects;
        }

        public async Task<List<Subject>> GetByCourse(int courseId)
        {
            List<SubjectEntity> subjectEntities = await _subjectRepository
                .GetByCourse(courseId);

            List<Subject> subjects = subjectEntities
                .Select(s => SubjectMapper.Map(s))
                .ToList();

            return subjects;
        }

        

        public async Task<Subject> Save(Subject subject)
        {
            SubjectEntity subjectEntity = SubjectMapper.Map(subject);
            subjectEntity = await _subjectRepository.Save(subjectEntity);

            subject = SubjectMapper.Map(subjectEntity);

            return subject;
        }

        public async Task<Subject> Update(Subject subject)
        {
            SubjectEntity subjectEntity = SubjectMapper.Map(subject);
            subjectEntity = await _subjectRepository.Update(subjectEntity);

            subject = SubjectMapper.Map(subjectEntity);

            return subject;
        }

        public async Task<Subject> UpdateAssignedTeacher(int subjectId, int teacherId)
        {
            TeacherEntity teacherEntity = await _teachertRepository.Get(teacherId);

            SubjectEntity subjectEntity = await _subjectRepository
                .UpdateAssignedTeacher(subjectId, teacherEntity);

            Subject subject = SubjectMapper.MapIncludingTeacher(subjectEntity);

            return subject;
        }

        public async Task<Subject> RemoveAssignedTeacher(int subjectId)
        {

            SubjectEntity subjectEntity = await _subjectRepository
                .UpdateAssignedTeacher(subjectId,null);

            Subject subject = SubjectMapper.MapIncludingTeacher(subjectEntity);

            return subject;
        }
    }
}
