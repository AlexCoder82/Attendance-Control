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
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IDatabaseTransaction _databaseTransaction;
        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository courseRepository,
                             ISubjectRepository subjectRepository,
                             IStudentRepository studentRepository,
                             ISchoolClassRepository schoolClassRepository,
                             IDatabaseTransaction databaseTransaction,
                             ILogger<CourseService> logger)
        {
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
            _studentRepository = studentRepository;
            _schoolClassRepository = schoolClassRepository;
            _databaseTransaction = databaseTransaction;
            _logger = logger;
        }


        /// <summary>
        ///     Asigna una asignatura a un curso y actualiza la relación de todos los 
        ///     alumnos del curso con la asignatura
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public async Task<bool> AssignSubject(int courseId, int subjectId)
        {

            bool isAssigned = await _courseRepository.AssignSubject(courseId, subjectId);

            return isAssigned;
        }

        public async Task<List<Course>> GetAll()
        {
            List<CourseEntity> courseEntities = await _courseRepository.GetAll();

            List<Course> courses = courseEntities
                .Select(c => CourseMapper.MapIncludingCycle(c))
                .ToList();

            return courses;
        }

        public async Task<bool> RemoveAssignedSubject(int courseId, int subjectId)
        {

            bool isRemoved = await _courseRepository.RenoveAssignedSubject(courseId, subjectId);

            
            return isRemoved;
        }
    }
}
