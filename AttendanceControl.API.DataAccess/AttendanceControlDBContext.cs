using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceControl.API.DataAccess
{
    /// <summary>
    ///     Configuración del contexto de la base de datos (Entity Framework)
    /// </summary>
    public class AttendanceControlDBContext : DbContext, IAttendanceControlDBContext
    {

        public AttendanceControlDBContext(DbContextOptions<AttendanceControlDBContext> options) : base(options)
        {

        }

        /// <summary>
        ///     Sets de todas las entidades
        /// </summary>
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

        /// <summary>
        ///     Creación del modelo
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Propriedades que tienen un valor por defecto en la base de datos
            modelBuilder.Entity<SchoolClassEntity>()
            .Property(sc => sc.IsCurrent)
            .HasDefaultValue(true);

            modelBuilder.Entity<StudentEntity>()
            .Property(s => s.TotalAbsences)
            .HasDefaultValue(0);

            modelBuilder.Entity<StudentEntity>()
             .Property(s => s.TotalDelays)
             .HasDefaultValue(0);

            modelBuilder.Entity<StudentEntity>()
             .Property(s => s.CourseId)
            .HasDefaultValue(null);


            // Primary keys de las relaciones many to many
            modelBuilder.Entity<SchoolClassStudentEntity>().HasKey(scs => new
            {
                scs.StudentId,
                scs.SchoolClassId
            });

            modelBuilder.Entity<StudentSubjectEntity>().HasKey(ss => new
            {
                ss.StudentId,
                ss.SubjectId
            });

            modelBuilder.Entity<CourseSubjectEntity>().HasKey(cs => new
            {
                cs.CourseId,
                cs.SubjectId
            });

            base.OnModelCreating(modelBuilder);

        }

    }
}
