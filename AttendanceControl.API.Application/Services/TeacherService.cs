using AttendanceControl.API.Application.Contracts.DTOs;
using AttendanceControl.API.Application.Contracts.IAuth;
using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.Mappers;
using AttendanceControl.API.Business.Enums;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuthService _authService;
        private readonly ISchoolClassService _schoolClassService;

        public TeacherService(ITeacherRepository teacherRepository,
                              IAuthService authService,
                              ISchoolClassService schoolClassService)
        {
            _teacherRepository = teacherRepository;
            _authService = authService;
            _schoolClassService = schoolClassService;
        }

        /// <summary>
        ///     Lista todos los profesores
        /// </summary>
        /// <returns>
        ///     Retorna una lista de objetos Teacher
        /// </returns>
        public async Task<List<Teacher>> GetAll()
        {

            List<TeacherEntity> teacherEntities = await _teacherRepository.GetAll();

            List<Teacher> teachers = teacherEntities
                .Select(t => TeacherMapper.Map(t)).ToList();

            return teachers;

        }

        /// <summary>
        ///     Crea un nuevo profesor
        /// </summary>
        /// <param name="teacher">
        ///     El objeto Teacher que contiene los datos del profesor
        /// </param>
        /// <exception cref="DniDuplicateEntryException">
        ///     Lanza DniDuplicateEntryException
        /// </exception>
        /// <returns>
        ///     Retorna el objeto Teacher creado con su id generado
        /// </returns>
        public async Task<Teacher> Save(Teacher teacher)//throw DniDuplicateEntryException
        {

            TeacherEntity teacherEntity = TeacherMapper.Map(teacher);

            teacherEntity = await _teacherRepository.Save(teacherEntity);

            teacher = TeacherMapper.Map(teacherEntity);

            return teacher;

        }

        /// <summary>
        ///     Actualiza un  profesor
        /// </summary>
        /// <param name="teacher">
        ///     El objeto Teacher que contiene los nuevos datos del profesor
        /// </param>
        /// <exception cref="DniDuplicateEntryException">
        ///     Lanza DniDuplicateEntryException
        /// </exception>
        /// <returns>
        ///     Retorna el objeto Teacher actualizado
        /// </returns>
        public async Task<Teacher> Update(Teacher teacher)
        {

            TeacherEntity teacherEntity = TeacherMapper.Map(teacher);

            teacherEntity = await _teacherRepository.Update(teacherEntity);

            teacher = TeacherMapper.Map(teacherEntity);

            return teacher;

        }

        /// <summary>
        ///     Abre una sesión de profesor si el dni es reconocido 
        ///     por el repositorio
        /// </summary>
        /// <param name="dni">
        ///     dni del profesor
        /// </param>
        /// <returns>
        ///     Retorna un objeto TeacherSignInResponse que contiene 
        ///     el id del profesor,su nombre, el token generado, 
        ///     el Role profesor y la lista de clases que imparte
        ///     el profesor "hoy"
        /// </returns>
        public async Task<TeacherSignInResponse> SignIn(string dni)
        {

            TeacherEntity teacherEntity = await _teacherRepository
                 .GetByDni(dni);

            //Token único de sessión
            string token = _authService
                        .GenerateToken(teacherEntity.Dni,
                             Role.TEACHER);

            //Lista de las clases del dia del profesor
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
       
    }
}
