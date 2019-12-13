using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceControl.API.DataAccess.Contracts.Entities
{
    [Table("person_data")]
    public  class PersonDataEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("dni", TypeName = "varchar(9)")]
        public string Dni { get; set; }

        [Required]
        [Column("firstname", TypeName = "varchar(255)")]
        public string FirstName { get; set; }

        [Required]
        [Column("lastname1", TypeName = "varchar(255)")]
        public string LastName1 { get; set; }

        [Column("lastname2", TypeName = "varchar(255)")]
        public string LastName2 { get; set; }

        public virtual StudentEntity StudentEntity { get; set; }
        public virtual TeacherEntity TeacherEntity { get; set; }
       
    }
}
