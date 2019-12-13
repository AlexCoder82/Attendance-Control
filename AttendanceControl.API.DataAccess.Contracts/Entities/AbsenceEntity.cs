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
        [Column("type", TypeName = "int")]
        public AbsenceType Type { get; set; }

        [Required]
        [ForeignKey("ScheduleEntity")]
        [Column("scheduleId", TypeName = "int")]
        public int ScheduleId { get; set; }

        [Required]
        [ForeignKey("StudentEntity")]
        [Column("studentId", TypeName = "int")]
        public int StudentId { get; set; }

        public virtual ScheduleEntity ScheduleEntity { get; set; }
        public virtual StudentEntity StudentEntity { get; set; }
    }
}
