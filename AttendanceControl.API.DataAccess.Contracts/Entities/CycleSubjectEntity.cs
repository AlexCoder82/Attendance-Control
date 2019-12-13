using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("cycle_has_subjects")]
    public class CycleSubjectEntity
    {
        [Key]
        [ForeignKey("CycleEntity")]
        [Column("cycleId")]
        public int CycleId { get; set; }

        [Key]
        [ForeignKey("SubjectEntity")]
        [Column("subjectId")]
        public int SubjectId { get; set; }

        public virtual CycleEntity CycleEntity { get; set; }
        public virtual SubjectEntity SubjectEntity { get; set; }

    }
}
