﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    /// <summary>
    ///    Entidad Alumno mapeada con la tabla "student"
    /// </summary>
    [Table("student")]
    public class StudentEntity 
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("dni", TypeName = "varchar(9)")]
        public string Dni { get; set; }

        [Required]
        [Column("firstname", TypeName = "varchar(255)")]
        public string FirstName { get; set; }

        [Required]
        [Column("lastname1", TypeName = "varchar(255)")]
        public string LastName1 { get; set; }

        [Column("lastname2", TypeName = "varchar(255)")]
        public string LastName2 { get; set; }

        [ForeignKey("CourseEntity")]
        [Column("course_id", TypeName = "int")]
        public int? CourseId { get; set; }

        [Required]
        [DefaultValue(0)]
        [Column("total_absences", TypeName = "int")]
        public int TotalAbsences { get; set; }

        [Required]
        [DefaultValue(0)]
        [Column("total_delays", TypeName = "int")]
        public int TotalDelays { get; set; }

        public virtual CourseEntity CourseEntity { get; set; }

        public virtual ICollection<StudentSubjectEntity> StudentSubjectEntities { get; set; }

        public virtual ICollection<SchoolClassStudentEntity> SchoolClassStudentEntities { get; set; }
       
        public virtual ICollection<AbsenceEntity> AbsenceEntities { get; set; }

    }
}
