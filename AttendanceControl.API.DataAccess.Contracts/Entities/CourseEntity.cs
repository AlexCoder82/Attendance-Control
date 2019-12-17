using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("course")]
    public class CourseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("year", TypeName = "int(1)")]
        public int Year { get; set; }

        [Required]
        [ForeignKey("CycleEntity")]
        [Column("cycle_id")]
        public int CycleId { get; set; }

        public virtual CycleEntity CycleEntity { get; set; }

        public virtual ICollection<CourseSubjectEntity> CourseSubjectEntities { get; set; }

        public virtual ICollection<SchoolClassEntity> SchoolClassEntities { get; set; }
    }
}
