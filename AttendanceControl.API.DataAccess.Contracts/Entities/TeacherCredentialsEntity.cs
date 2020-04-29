using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("teacher_credentials")]
    public class TeacherCredentialsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("username", TypeName = "varchar(32)")]
        public string Username { get; set; }

        [Column("password", TypeName = "varchar(32)")]
        public string Password { get; set; }

        public virtual TeacherEntity TeacherEntity { get; set; }
    }
}
