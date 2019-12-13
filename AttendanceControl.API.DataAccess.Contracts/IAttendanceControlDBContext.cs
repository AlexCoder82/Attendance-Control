using AttendanceControl.API.DataAccess.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts
{
    public interface IAttendanceControlDBContext
    {

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
    }
}
