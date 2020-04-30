using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("course_has_subjects")]
    public class CourseSubjectEntity
    {

        [Key]
        [ForeignKey("CourseEntity")]
        [Column("course_id")]
        public int CourseId { get; set; }

        [Key]
        [ForeignKey("SubjectEntity")]
        [Column("subject_id")]
        public int SubjectId { get; set; }

        public virtual CourseEntity CourseEntity { get; set; }

        public virtual SubjectEntity SubjectEntity { get; set; }

    }
}
