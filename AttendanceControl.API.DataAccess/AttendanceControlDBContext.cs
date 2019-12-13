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
        public DbSet<CycleEntity> CycleEntities { get; set; }
        public DbSet<CycleSubjectEntity> CycleSubjectEntities { get; set; }
        public DbSet<ScheduleEntity> ScheduleEntities { get; set; }
        public DbSet<TeacherEntity> TeacherEntities { get; set; }
        public DbSet<SubjectEntity> SubjectEntities { get; set; }
        public DbSet<StudentEntity> StudentEntities { get; set; }
        public DbSet<PersonDataEntity> PersonDataEntities { get; set; }
        public DbSet<StudentSubjectEntity> PersonSubjectEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// CYCLESUBJECTENTITY PRIMARY KEY

            modelBuilder.Entity<CycleSubjectEntity>().HasKey(cs => new
            {
                cs.CycleId,
                cs.SubjectId
            });

            /// STUDENTSUBJECTENTITY PRIMARY KEY

            modelBuilder.Entity<StudentSubjectEntity>().HasKey(ss => new
            {
                ss.StudentId,
                ss.SubjectId
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

            //DEFAULT INSERTS
            // SaveDefaultAdmin();

        }

        //private void SaveDefaultAdmin()
        //{
        //    AdminEntities.AddAsync(new AdminEntity
        //    {
        //        AdminName = "admin1",
        //        Password = "admin1"
        //    });

        //    this.SaveChangesAsync();
        //}
    }
}
