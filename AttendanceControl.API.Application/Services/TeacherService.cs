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
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ILogger<TeacherService> _logger;

        public TeacherService(ITeacherRepository teacherRepository, ILogger<TeacherService> logger)
        {
            _teacherRepository = teacherRepository;
            _logger = logger;
        }

        public async Task<List<Teacher>> GetAll()
        {
            List<TeacherEntity> teacherEntities = await _teacherRepository.GetAll();


            List<Teacher> teachers = teacherEntities
                .Select(t => TeacherMapper.Map(t)).ToList();
            return teachers;
        }

        public async Task<Teacher> Save(Teacher teacher)
        {
            TeacherEntity teacherEntity = TeacherMapper.Map(teacher);
            teacherEntity = await _teacherRepository.Save(teacherEntity);
            teacher = TeacherMapper.Map(teacherEntity);
            return teacher;
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
