using AttendanceControl.API.DataAccess.Contracts.Enums;
using System;
using System.Collections.Generic;
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
        [Column("day", TypeName = "int(1)")]
        public Day Day { get; set; }

        [Required]
        [Column("start", TypeName = "timestamp")]
        public DateTime Start { get; set; }

        [Required]
        [Column("end", TypeName = "timestamp")]
        public DateTime End { get; set; }
     
    }
}
