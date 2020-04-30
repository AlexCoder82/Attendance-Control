using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("cycle")]
    public class CycleEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Required]
        [ForeignKey("ShiftEntity")]
        [Column("shift_id")]
        public int ShiftId { get; set; }

        public virtual ICollection<CourseEntity> CourseEntities { get; set; }

        public virtual ShiftEntity ShiftEntity { get; set; }

    }
}
