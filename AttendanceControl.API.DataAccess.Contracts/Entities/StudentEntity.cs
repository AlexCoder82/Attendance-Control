using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("student")]
    public class StudentEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [ForeignKey("CourseEntity")]
        [Column("course_id", TypeName = "int")]
        public int CourseId { get; set; }

        [Required]
        [DefaultValue(0)]
        [Column("total_absences", TypeName = "int")]
        public int TotalAbsences { get; set; }

        [Required]
        [DefaultValue(0)]
        [Column("total_delays", TypeName = "int")]
        public int TotalDelays { get; set; }

        [Required]
        [ForeignKey("PersonDataEntity")]
        [Column("person_data_id", TypeName = "int")]
        public int PersonDataId { get; set; }

        public virtual CourseEntity CourseEntity { get; set; }
        public virtual PersonDataEntity PersonDataEntity { get; set; }
        public virtual ICollection<StudentSubjectEntity> StudentSubjectEntities { get; set; }

        public virtual ICollection<SchoolClassStudentEntity> SchoolClassStudentEntities { get; set; }
        public virtual ICollection<AbsenceEntity> AbsenceEntities { get; set; }
    }
}
