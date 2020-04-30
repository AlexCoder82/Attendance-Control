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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository,
                              ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        ///     Retorna una página de alumnos filtrados por apellido
        /// </summary>
        /// <param name="lastName">
        ///     El apellido que se busca
        /// </param>
        /// <param name="page">
        ///     La página pedida
        /// </param>
        /// <returns>
        ///     Retorna una lista de objetos Student que contienen cada
        ///     uno, un objeto Course y una lista de objetos Subject
        /// </returns>
        public async Task<List<Student>> GetByPageLikeLastNameIncludingCourseAndSubjects(string lastName, int page)
        {

            List<StudentEntity> studentEntities = new List<StudentEntity>();

            //Según si se filtra por apellido o no, se llama a un método 
            //distincto del repositorio
            if (lastName is null || lastName.Length == 0)
            {
                studentEntities = await _studentRepository
                   .GetByPageIncludingCourseAndSubjects(page);
            }
            else
            {
                studentEntities = await _studentRepository
                   .GetByPageLikeLastNameIncludingCourseAndSubjects(lastName, page);
            }

            List<Student> students = studentEntities
                .Select(s => StudentMapper
                    .MapIncludingAssignedCourseAndSubjects(s))
                .ToList();

            return students;

        }

        /// <summary>
        ///     Crea un nuevo alumno
        /// </summary>
        /// <param name="student">
        ///     El objeto Student que contiene los datos del alumno
        /// </param>
        /// <exception cref="DniDuplicateEntryException">
        ///     Lanza DniDuplicateEntryException 
        /// </exception>
        /// <returns>
        ///     El objeto Student con su id generado
        /// </returns>
        public async Task<Student> Save(Student student)//throw DniDuplicateEntryException
        {

            StudentEntity studentEntity = StudentMapper.Map(student);

            studentEntity = await _studentRepository.Save(studentEntity);

            student = StudentMapper.Map(studentEntity);

            return student;

        }

        /// <summary>
        ///     Actualiza un alumno
        /// </summary>
        /// <param name="student">
        ///     El objeto Student con los nuevos datos del alumno
        /// </param>
        /// <exception cref="DniDuplicateEntryException">
        ///     Lanza DniDuplicateEntryException
        /// </exception>
        /// <returns>
        ///     Retorna el objeto Student actualizado
        /// </returns>
        public async Task<Student> Update(Student student)//throw DniDuplicateEntryException
        {

            StudentEntity studentEntity = StudentMapper.Map(student);

            studentEntity = await _studentRepository.Update(studentEntity);

            student = StudentMapper.Map(studentEntity);

            return student;

        }

        /// <summary>
        ///     Asigna un nuevo curso a un alumno
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno
        /// </param>
        /// <param name="courseId">
        ///     El id del curso
        /// </param>
        /// <returns>
        ///     Retorna un objeto Student que contiene la lista de 
        ///     asignaturas que cursa
        /// </returns>
        public async Task<Student> UpdateCourse(int studentId, int courseId)
        {

            await _studentRepository.UpdateCourse(studentId, courseId);

            StudentEntity studentEntity = await _studentRepository
                .GetIncludingSubjects(studentId);

            Student student = StudentMapper
                .MapIncludingAssignedCourseAndSubjects(studentEntity);

            return student;

        }

        /// <summary>
        ///     Retira la asignación de curso a un alumno
        /// </summary>
        /// <param name="studentId">
        ///     EL id del alumno
        /// </param>
        /// <returns>
        ///     Retorna true
        /// </returns>
        public async Task<bool> RemoveCourse(int studentId)
        {

            await _studentRepository.RemoveCourse(studentId);

            return true;

        }

        /// <summary>
        ///     Actualiza la lista de asignaturas asociadas a un alumno
        /// </summary>
        /// <param name="studentId">
        ///     El id del alumno 
        /// </param>
        /// <param name="subjectIds">
        ///     Un array de ids de las asignaturas
        /// </param>
        /// <returns>
        ///     Retorna un objeto Student que contiene la lista de 
        ///     asignaturas que cursa
        /// </returns>
        public async Task<Student> UpdateSubjects(int studentId, int[] subjectIds)
        {

            await _studentRepository.UpdateSubjects(studentId, subjectIds);

            //Recupera el alumno con sus nuevas asignaturas 
            StudentEntity studentEntity = await _studentRepository
                .GetIncludingSubjects(studentId);

            Student student = StudentMapper.MapIncludingAssignedCourseAndSubjects(studentEntity);

            return student;

        }

    }
}
