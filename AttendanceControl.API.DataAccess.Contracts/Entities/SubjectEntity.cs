using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("subject")]
    public class SubjectEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [ForeignKey("TeacherEntity")]//OPTIONAL
        [Column("teacher_id", TypeName = "int)")]
        public int? TeacherId { get; set; }

        public TeacherEntity TeacherEntity { get; set; }
        public virtual ICollection<CourseSubjectEntity> CourseSubjectEntities { get; set; }
        public virtual ICollection<StudentSubjectEntity> StudentSubjectEntities { get; set; }

        public virtual ICollection<SchoolClassEntity> SchoolClassEntities { get; set; }

    }
}
