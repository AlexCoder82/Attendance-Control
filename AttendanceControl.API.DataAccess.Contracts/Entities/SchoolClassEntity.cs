using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("school_class")]
    public class SchoolClassEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("CourseEntity")]
        [Column("course_id", TypeName = "int")]
        public int CourseId  { get; set; }

        [Required]
        [ForeignKey("SubjectEntity")]
        [Column("subject_id", TypeName = "int")]
        public int SubjectId { get; set; }

        [Required]
        [ForeignKey("ScheduleEntity")]
        [Column("schedule_id", TypeName = "int")]
        public int ScheduleId { get; set; }

        [Required]
        [Column("day_of_week", TypeName = "int(1)")]
        public DayOfWeek Day { get; set; }

        [DefaultValue("true")]
        [Column("is_current", TypeName = "boolean")]
        public bool IsCurrent { get; set; }

        public virtual CourseEntity CourseEntity { get; set; }

        public virtual SubjectEntity SubjectEntity { get; set; }

        public virtual ScheduleEntity ScheduleEntity { get; set; }

        public virtual ICollection<SchoolClassStudentEntity> SchoolClassStudentEntities { get; set; }
    
    }
}
