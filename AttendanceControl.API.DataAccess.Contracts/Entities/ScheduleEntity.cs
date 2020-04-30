
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("schedule")]
    public class ScheduleEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
     
        [Required]
        [Column("start", TypeName = "time")]
        public TimeSpan Start { get; set; }

        [Required]
        [Column("end", TypeName = "time")]
        public TimeSpan End { get; set; }

        [Required]
        [ForeignKey("ShiftEntity")]
        [Column("shift_id")]
        public int ShiftId { get; set; }

        public virtual ShiftEntity ShiftEntity { get; set; }

    }
}
