using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.Mappers;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Services
{
    /// <summary>
    ///     Lógica relacionada con los cursos 
    /// </summary>
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository courseRepository,
                             ISubjectRepository subjectRepository,
                             IStudentRepository studentRepository,
                             ISchoolClassRepository schoolClassRepository,
                             ILogger<CourseService> logger)
        {
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
            _studentRepository = studentRepository;
            _schoolClassRepository = schoolClassRepository;
            _logger = logger;
        }


        /// <summary>
        ///     Asigna una asignatura a un curso 
        /// </summary>
        /// <param name="courseId">
        ///     El id del curso
        /// </param>
        /// <param name="subjectId">
        ///     EL id de la asignatura
        /// </param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        public async Task<bool> AssignSubject(int courseId, int subjectId)
        {

            bool isAssigned = await _courseRepository.AssignSubject(courseId, subjectId);

            return isAssigned;
        }

        /// <summary>
        ///     Lista todos los cursos incluyendo el ciclo formativo
        ///     al que pertenecen 
        /// </summary>
        /// <returns>
        ///     Retorna una lista de objetos Course que contienen 
        ///     cada uno un objeto Cycle
        /// </returns>
        public async Task<List<Course>> GetAll()
        {
            List<CourseEntity> courseEntities = await _courseRepository.GetAll();

            List<Course> courses = courseEntities
                .Select(c => CourseMapper.MapIncludingCycle(c))
                .ToList();

            return courses;
        }

        /// <summary>
        ///     Retira una asignatura de un curso
        /// </summary>
        /// <param name="courseId">
        ///     EL id del curso
        /// </param>
        /// <param name="subjectId">
        ///     El id de la asignatura
        /// </param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        public async Task<bool> RemoveAssignedSubject(int courseId, int subjectId)
        {

            bool isRemoved = await _courseRepository.RenoveAssignedSubject(courseId, subjectId);

            
            return isRemoved;
        }

    }
}
