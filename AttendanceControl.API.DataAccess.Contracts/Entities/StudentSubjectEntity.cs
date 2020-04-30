﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("student_has_subjects")]
    public class StudentSubjectEntity
    {

        [Key]
        [ForeignKey("StudentEntity")]
        [Column("student_id")]
        public int StudentId { get; set; }

        [Key]
        [ForeignKey("SubjectEntity")]
        [Column("subject_id")]
        public int SubjectId { get; set; }

        public virtual StudentEntity StudentEntity { get; set; }

        public virtual SubjectEntity SubjectEntity { get; set; }

    }
}
