using AttendanceControl.API.DataAccess.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts
{
    /// <summary>
    ///     Contratos del contexto de la base de datos(Entity Framework)
    /// </summary>
    public interface IAttendanceControlDBContext
    {

        public DbSet<AbsenceEntity> AbsenceEntities { get; set; }
        public DbSet<ShiftEntity> ShiftEntities { get; set; }
        public DbSet<AdminEntity> AdminEntities { get; set; }
        public DbSet<CourseEntity> CourseEntities { get; set; }
        public DbSet<SchoolClassEntity> SchoolClassEntities { get; set; }
        public DbSet<ScheduleEntity> ScheduleEntities { get; set; }
        public DbSet<TeacherEntity> TeacherEntities { get; set; }
        public DbSet<SubjectEntity> SubjectEntities { get; set; }
        public DbSet<StudentEntity> StudentEntities { get; set; }
        public DbSet<CycleEntity> CycleEntities { get; set; }
        public DbSet<SchoolClassStudentEntity> SchoolClassStudentEntities { get; set; }
        public DbSet<StudentSubjectEntity> StudentSubjectEntities { get; set; }
        public DbSet<CourseSubjectEntity> CourseSubjectEntities { get; set; }
        public  DatabaseFacade Database { get;  }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
   
    }
}
