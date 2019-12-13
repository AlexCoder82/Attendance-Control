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

        [ForeignKey("TeacherEntity")]
        [Column("teacherId", TypeName = "int")]
        public int TeacherId { get; set; }

        public virtual ICollection<CycleSubjectEntity> CycleSubjectEntities { get; set; }
        public virtual TeacherEntity TeacherEntity { get; set; }
        public virtual ICollection<ScheduleEntity> ScheduleEntities { get; set; }
    }
}
