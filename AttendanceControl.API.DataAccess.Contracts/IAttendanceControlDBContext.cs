using AttendanceControl.API.DataAccess.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts
{
    public interface IAttendanceControlDBContext
    {
        public DbSet<AbsenceEntity> AbsenceEntities { get; set; }
        public DbSet<AdminEntity> AdminEntities { get; set; }
        public DbSet<CourseEntity> CourseEntities { get; set; }
        public DbSet<SchoolClassEntity> SchoolClassEntities { get; set; }
        public DbSet<ScheduleEntity> ScheduleEntities { get; set; }
        public DbSet<TeacherEntity> TeacherEntities { get; set; }
        public DbSet<SubjectEntity> SubjectEntities { get; set; }
        public DbSet<StudentEntity> StudentEntities { get; set; }
        public DbSet<PersonDataEntity> PersonDataEntities { get; set; }
        public DbSet<CycleEntity> CycleEntities { get; set; }
        public DbSet<SchoolClassStudentEntity> SchoolClassStudentEntities { get; set; }

        public DbSet<StudentSubjectEntity> StudentSubjectEntities { get; set; }
        public DbSet<CourseSubjectEntity> CourseSubjectEntities { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        public EntityEntry Update(object entity);
    }
}
