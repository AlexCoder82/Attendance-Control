using AttendanceControl.API.DataAccess.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("absence")]
    public class AbsenceEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("type", TypeName = "int(1)")]
        public AbsenceType Type { get; set; }

        [Required]
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("SchoolClassEntity")]
        [Column("school_class_id", TypeName = "int")]
        public int SchoolClassId { get; set; }

        [Required]
        [ForeignKey("StudentEntity")]
        [Column("student_id", TypeName = "int")]
        public int StudentId { get; set; }

        [Required]
        [Column("is_excused", TypeName = "boolean")]
        public bool IsExcused { get; set; }

        public virtual SchoolClassEntity SchoolClassEntity { get; set; }
        public virtual StudentEntity StudentEntity { get; set; }
    }
}
