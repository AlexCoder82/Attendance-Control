using AttendanceControl.API.Application.Contracts.DTOs;
using AttendanceControl.API.Application.Contracts.IAuth;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.Mappers;
using AttendanceControl.API.Business.Enums;
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
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuthService _authService;
        private readonly ISchoolClassService _schoolClassService;
        private readonly ILogger<TeacherService> _logger;

        public TeacherService(ITeacherRepository teacherRepository,
                              IAuthService authService,
                              ISchoolClassService schoolClassService,
                              ILogger<TeacherService> logger)
        {
            _teacherRepository = teacherRepository;
            _authService = authService;
            _schoolClassService = schoolClassService;
            _logger = logger;
        }

        public async Task<List<Teacher>> GetAll()
        {
            List<TeacherEntity> teacherEntities = await _teacherRepository.GetAll();

            List<Teacher> teachers = teacherEntities
                .Select(t => TeacherMapper.Map(t)).ToList();

            return teachers;
        }

        public async Task<Teacher> Register(string dni, TeacherCredentials teacherCredentials)
        {
            TeacherEntity teacherEntity = await _teacherRepository.GetByDni(dni);

            TeacherCredentialsEntity teacherCredentialsEntity = new TeacherCredentialsEntity()
            {
                Username= teacherCredentials.Username,
                Password = teacherCredentials.Password
            };
            await _teacherRepository.Register(teacherEntity, teacherCredentialsEntity);

            teacherEntity.TeacherCredentialsEntity = teacherCredentialsEntity;

            Teacher teacher = TeacherMapper.MapIncludingCredentials(teacherEntity);
            return teacher;
        }

        public async Task<Teacher> Save(Teacher teacher)
        {
            TeacherEntity teacherEntity = TeacherMapper.Map(teacher);
            teacherEntity = await _teacherRepository.Save(teacherEntity);
            teacher = TeacherMapper.Map(teacherEntity);
            return teacher;
        }

        public async Task<TeacherSignInResponse> SignIn(string dni)
        {
            TeacherEntity teacherEntity = await _teacherRepository
                 .GetByDni(dni);

            string token = _authService
                        .GenerateToken(teacherEntity.Dni,
                             Role.TEACHER);

            List<SchoolClass> schoolClasses = await _schoolClassService
                .GetByTeacher(teacherEntity.Id);
            TeacherSignInResponse signInResponse = new TeacherSignInResponse()
            {
                TeacherId = teacherEntity.Id,
                FirstName = teacherEntity.FirstName,
                Token = token,
                Role = Role.TEACHER,
                SchoolClasses = schoolClasses
            };

            return signInResponse;
        }

        public async Task<Teacher> Update(Teacher teacher)
        {
            TeacherEntity teacherEntity = TeacherMapper.Map(teacher);
            teacherEntity = await _teacherRepository.Update(teacherEntity);
            teacher = TeacherMapper.Map(teacherEntity);
            return teacher;
        }
    }
}
