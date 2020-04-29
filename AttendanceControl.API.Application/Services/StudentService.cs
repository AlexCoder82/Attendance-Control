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
        private readonly IDatabaseTransaction _databaseTransaction;
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository studentRepository,
                              ISchoolClassRepository schoolClassRepository,
                             IDatabaseTransaction databaseTransaction,
                             ICourseRepository courseRepository,
                             ISubjectRepository subjectRepository,
        ILogger<StudentService> logger)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _schoolClassRepository = schoolClassRepository;
            _subjectRepository = subjectRepository;
            _databaseTransaction = databaseTransaction;
            _logger = logger;
        }


        public async Task<List<Student>> GetByPageLikeLastNameIncludingCourseAndSubjects(string lastName, int page)
        {
            List<StudentEntity> studentEntities = new List<StudentEntity>();
            if (lastName is null || lastName.Length == 0)
            {
                 studentEntities = await _studentRepository
                .GetByPageIncludingCourseAndSubjects( page);
            }
            else
            {
                 studentEntities = await _studentRepository
                .GetByPageLikeLastNameIncludingCourseAndSubjects(lastName, page);
            }
            

            List<Student> students = studentEntities.Select(s => StudentMapper.MapIncludingAssignedSubjects(s)).ToList();

            return students;
        }

        public async Task<Student> Save(Student student)
        {
            StudentEntity studentEntity = StudentMapper.Map(student);

            studentEntity = await _studentRepository.Save(studentEntity);



            student = StudentMapper.Map(studentEntity);

            return student;
        }

        public async Task<Student> Update(Student student)
        {
            StudentEntity studentEntity = StudentMapper.Map(student);

            studentEntity = await _studentRepository.Update(studentEntity);

            student = StudentMapper.Map(studentEntity);

            return student;
        }

        public async Task<Student> UpdateCourse(int studentId, int courseId)
        {
  
                 await _studentRepository
                    .UpdateCourse(studentId, courseId);
               
                StudentEntity studentEntity = await _studentRepository
                    .GetIncludingSubjects(studentId);

                Student student = StudentMapper.MapIncludingAssignedSubjects(studentEntity);

                return student;

          
        }


        public async Task<bool> RemoveCourse(int studentId)
        {
            await _studentRepository
                   .RemoveCourse(studentId);

            return true;

        }


        public async Task<Student> UpdateSubjects(int studentId, int[] subjectIds)
        {

            await _studentRepository.UpdateSubjects(studentId, subjectIds);

            //Recupera el alumno con sus nuevas asignaturas 

            StudentEntity studentEntity = await _studentRepository.GetIncludingSubjects(studentId);

            Student student =  StudentMapper.MapIncludingAssignedSubjects(studentEntity);

            return student;
          /*  try
            {
                await _databaseTransaction.Begin();
                List<SubjectEntity> subjectEntities = new List<SubjectEntity>();

                foreach (int id in subjectIds)
                {
                    SubjectEntity subjectEntity = await _subjectRepository.Get(id);
                    subjectEntities.Add(subjectEntity);
                }

                StudentEntity studentEntity = await _studentRepository
                    .GetIncludingCourseAndSubjectsAndSchoolCLasses(studentId);

                List<SchoolClassEntity> schoolClassEntities = await _schoolClassRepository
                    .GetByCourse(studentEntity.CourseEntity.Id);

                List<SchoolClassEntity> assignedSchoolClassEntities = new List<SchoolClassEntity>();

                if (schoolClassEntities.Count > 0)
                {
                    foreach (SchoolClassEntity sc in schoolClassEntities)
                    {
                        foreach (SubjectEntity s in subjectEntities)
                        {
                            if (sc.SubjectId == s.Id)
                            {
                                assignedSchoolClassEntities.Add(sc);
                            }
                        }
                    }
                }

                studentEntity = await _studentRepository.UpdateShoolClasses(studentEntity, assignedSchoolClassEntities);
                studentEntity = await _studentRepository.UpdateSubjects(studentEntity, subjectEntities);

                _databaseTransaction.Commit();


                Student student = StudentMapper.MapIncludingAssignedSubjects(studentEntity);

                return student;
            }
            catch (Exception ex)
            {
                _databaseTransaction.Rollback();
                throw ex;
            }*/
        }

        public async Task<List<Student>> GetByCourse(int courseId)
        {
            List<StudentEntity> studentEntities = await _studentRepository
                .GetByCourse(courseId);
            List<Student> students = new List<Student>();
            students = studentEntities.Select(s => StudentMapper.Map(s)).ToList();

            return students;
        }

        public async Task<List<Student>> GetByPageIncludingCourseAndSubjects(int page)
        {
            List<StudentEntity> studentEntities = await _studentRepository
               .GetByPageIncludingCourseAndSubjects(page);

            List<Student> students = studentEntities.Select(s => StudentMapper.MapIncludingAssignedSubjects(s)).ToList();

            return students;
        }
    }
}
