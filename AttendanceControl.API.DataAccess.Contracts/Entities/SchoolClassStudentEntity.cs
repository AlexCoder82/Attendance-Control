
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("school_class_has_students")]
    public class SchoolClassStudentEntity
    {

        [Key]
        [ForeignKey("StudentEntity")]
        [Column("student_id")]
        public int StudentId { get; set; }

        [Key]
        [ForeignKey("SchoolClassEntity")]
        [Column("school_class_id")]
        public int SchoolClassId { get; set; }

        public virtual StudentEntity StudentEntity { get; set; }

        public virtual SchoolClassEntity SchoolClassEntity { get; set; }

    }
}
