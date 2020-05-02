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
    ///     Lógica relacionada con las asignaturas
    /// </summary>
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teachertRepository;

        public SubjectService(
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository)
        {
            _subjectRepository = subjectRepository;
            _teachertRepository = teacherRepository;
        }

        /// <summary>
        ///     Lista todas las asignaturas con el profesor asignado a cada una
        /// </summary>
        /// <returns>
        ///     Retorna una lista de objetos Subject que contienen cada uno
        ///     un objeto Teacher
        /// </returns>
        public async Task<List<Subject>> GetAllIncludingAssignedTeacher()
        {

            List<SubjectEntity> subjectEntities = await _subjectRepository
                .GetAllIncludingAssignedTeacher();

            List<Subject> subjects = subjectEntities
                .Select(s => SubjectMapper.MapIncludingTeacher(s))
                .ToList();

            return subjects;

        }

        /// <summary>
        ///     Lista todas las asignaturas de un curso
        /// </summary>
        /// <param name="courseId">
        ///     El id del curso
        /// </param>
        /// <returns>
        ///     Retorna una lista de objetos Subject
        /// </returns>
        public async Task<List<Subject>> GetByCourse(int courseId)
        {

            List<SubjectEntity> subjectEntities = await _subjectRepository
                .GetByCourseIncludingAssignedTeacher(courseId);

            List<Subject> subjects = subjectEntities
                .Select(s => SubjectMapper.MapIncludingTeacher(s))
                .ToList();

            return subjects;

        }

        /// <summary>
        ///     Crea una nueva asignatura
        /// </summary>
        /// <param name="subject">
        ///     Un objeto Subject que contiene los datos de la asignatura
        /// </param>
        /// <exception cref="SubjectNameDuplicateEntryException">
        ///     Lanza SubjectNameDuplicateEntryException
        /// </exception>
        /// <returns>
        ///     El objeto Subject creado con el id generado
        /// </returns>
        public async Task<Subject> Save(Subject subject)//throw SubjectNameDuplicateEntryException
        {

            SubjectEntity subjectEntity = SubjectMapper.Map(subject);

            subjectEntity = await _subjectRepository.Save(subjectEntity);

            subject = SubjectMapper.Map(subjectEntity);

            return subject;

        }

        /// <summary>
        ///     Actualiza una asignatura
        /// </summary>
        /// <param name="subject">
        ///     Un objeto Subject que contiene los nuevos datos de la asignatura
        /// </param>
        /// <exception cref="SubjectNameDuplicateEntryException">
        ///     Lanza SubjectNameDuplicateEntryException
        /// </exception>
        /// <returns>
        ///     El objeto Subject actualizado 
        /// </returns>
        public async Task<Subject> Update(Subject subject)
        {

            SubjectEntity subjectEntity = SubjectMapper.Map(subject);

            subjectEntity = await _subjectRepository.Update(subjectEntity);

            subject = SubjectMapper.Map(subjectEntity);

            return subject;

        }

        /// <summary>
        ///     Cambia el profesor asignado a una asignatura
        /// </summary>
        /// <param name="subjectId">
        ///     El id de la asignatura
        /// </param>
        /// <param name="teacherId">
        ///     El id del profesor
        /// </param>
        /// <returns>
        ///     Retorna el objeto asignatura que contiene 
        ///     el nuevo objeto Teacher asignado
        /// </returns>
        public async Task<Subject> UpdateAssignedTeacher(int subjectId, int teacherId)
        {

            TeacherEntity teacherEntity = await _teachertRepository.Get(teacherId);

            SubjectEntity subjectEntity = await _subjectRepository
                    .UpdateAssignedTeacher(subjectId, teacherEntity);

            Subject subject = SubjectMapper.MapIncludingTeacher(subjectEntity);

            return subject;

        }

        /// <summary>
        ///     Retira la asignacion de profesor a una asignatura
        /// </summary>
        /// <param name="subjectId">
        ///     El id de la asignatura
        /// </param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        public async Task<bool> RemoveAssignedTeacher(int subjectId)
        {

            SubjectEntity subjectEntity = await _subjectRepository
                .UpdateAssignedTeacher(subjectId,null);

            return true;

        }
    }
}
