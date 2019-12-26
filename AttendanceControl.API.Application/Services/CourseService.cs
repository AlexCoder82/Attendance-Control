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
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository courseRepository, ISchoolClassRepository schoolClassRepository, ILogger<CourseService> logger)
        {
            _courseRepository = courseRepository;
            _schoolClassRepository = schoolClassRepository;
            _logger = logger;
        }

        public async Task<List<SchoolClass>> GetSchoolClasses(int courseId)
        {
            List<SchoolClassEntity> schoolClassEntities = await _schoolClassRepository.GetByCourse(courseId);

            List<SchoolClass> schoolClasses = schoolClassEntities
                .Select(sc => SchoolClassMapper.Map(sc)).ToList();

            return schoolClasses;
        }

        public async Task<Course> UpdateCourse(Course course)
        {
            CourseEntity courseEntity = CourseMapper.MapIncludingSubjects(course);
            Console.WriteLine("\n\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA " + "\n\n");

            courseEntity.CourseSubjectEntities.ForEach(cs =>
            {

                Console.WriteLine("\n\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA " + cs.SubjectId + "\n\n");
            });

            courseEntity = await _courseRepository.Update(courseEntity);

            //  courseEntity = await _courseRepository.Get(courseEntity.Id);
            // Console.WriteLine("\n\nAAAAAAAAAAAAAAAAAAAAAAAAAA" +cycleEntity.Id);
            //course = CourseMapper.MapIncludingSubjects(courseEntity);

            return course;
        }
    }
}
