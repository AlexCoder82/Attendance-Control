using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("teacher")]
    public class TeacherEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

  
        [Column("username", TypeName = "varchar(32)")]
        public string Username { get; set; }


        [Column("password", TypeName = "varchar(32)")]
        public string Password { get; set; }

        [Required]
        [ForeignKey("PersonDataEntity")]
        [Column("person_data_id", TypeName = "int")]
        public int PersonDataId { get; set; }

        public virtual PersonDataEntity PersonDataEntity {get;set;}
        //public virtual ICollection<SchoolClassEntity> SchoolClassEntities { get; set; }

        public virtual ICollection<SubjectEntity> SubjectEntities { get; set; }


    }
}
