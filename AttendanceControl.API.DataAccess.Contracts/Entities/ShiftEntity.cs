using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("shift")]
    public class ShiftEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("description", TypeName = "varchar(255)")]
        public string Description { get; set; }

        public virtual ICollection<CycleEntity> CycleEntities { get; set; }
        public virtual ICollection<ScheduleEntity> ScheduleEntities { get; set; }
    }
}
