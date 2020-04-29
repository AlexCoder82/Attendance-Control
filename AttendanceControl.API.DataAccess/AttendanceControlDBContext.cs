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

        }

       
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
        public DbSet<TeacherCredentialsEntity> TeacherCredentialsEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            /// TEACHER HAS CREDENTIALS

            modelBuilder.Entity<TeacherCredentialsEntity>()
            .HasOne(tc =>tc.TeacherEntity)
            .WithOne(t => t.TeacherCredentialsEntity)
            .HasForeignKey<TeacherEntity>(t => t.CredentialsId);




            base.OnModelCreating(modelBuilder);


        }


    }
}
