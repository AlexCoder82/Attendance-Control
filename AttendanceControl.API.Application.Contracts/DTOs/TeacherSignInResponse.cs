using AttendanceControl.API.Business.Models;
using System.Collections.Generic;

namespace AttendanceControl.API.Application.Contracts.DTOs
{
    /// <summary>
    ///     Objeto que se instancia y retorna cuando un 
    ///     profesor abre una sesión
    /// </summary>
    public class TeacherSignInResponse
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
        public  string Role { get; set; }
        public List<SchoolClass> SchoolClasses { get; set; }
    }
}
