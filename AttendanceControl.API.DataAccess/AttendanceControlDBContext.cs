using AttendanceControl.API.DataAccess.Contracts;
using AttendanceControl.API.DataAccess.Contracts.Entities;

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AttendanceControl.API.DataAccess
{
    public class AttendanceControlDBContext : DbContext, IAttendanceControlDBContext
    {


        public AttendanceControlDBContext(DbContextOptions<AttendanceControlDBContext> options) : base(options)
        {
            Console.WriteLine("AAAAAAAAA");
        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SchoolClassEntity>()
            .Property(sc => sc.IsCurrent)
            .HasDefaultValue(true);

            /// STUDENTSCHOOLCLASSTENTITY PRIMARY KEY

            modelBuilder.Entity<SchoolClassStudentEntity>().HasKey(scs => new
            {
                scs.StudentId,
                scs.SchoolClassId
            });

            /// STUDENTSUBJECTENTITY PRIMARY KEY

            modelBuilder.Entity<StudentSubjectEntity>().HasKey(ss => new
            {
                ss.StudentId,
                ss.SubjectId
            });

            /// CYCLESUBJECTENTITY PRIMARY KEY

            modelBuilder.Entity<CourseSubjectEntity>().HasKey(cs => new
            {
                cs.CourseId,
                cs.SubjectId
            });

            /// ONE TO ONE STUDENT/PERSONDATA

            modelBuilder.Entity<PersonDataEntity>()
            .HasOne(pd => pd.StudentEntity)
            .WithOne(s => s.PersonDataEntity)
            .HasForeignKey<StudentEntity>(s => s.PersonDataId);

            /// ONE TO ONE TEACHER/PERSONDATA

            modelBuilder.Entity<PersonDataEntity>()
            .HasOne(pd => pd.TeacherEntity)
            .WithOne(t => t.PersonDataEntity)
            .HasForeignKey<TeacherEntity>(t => t.PersonDataId);

            base.OnModelCreating(modelBuilder);


        }

        
    }
}
