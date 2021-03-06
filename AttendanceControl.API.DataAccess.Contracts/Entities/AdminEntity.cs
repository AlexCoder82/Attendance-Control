﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    /// <summary>
    ///    Entidad Admin mapeada con la tabla "administrator"
    /// </summary>
    [Table("administrator")]
    public class AdminEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("admin_name", TypeName = "varchar(32)")]
        public string AdminName { get; set; }

        [Required]
        [Column("password", TypeName = "varchar(32)")]
        public string Password { get; set; }

    }
}
