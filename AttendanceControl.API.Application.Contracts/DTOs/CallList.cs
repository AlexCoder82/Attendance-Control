using AttendanceControl.API.Business.Models;

namespace AttendanceControl.API.Application.Contracts.DTOs
{
    /// <summary>
    ///     Objeto que contiene la información de cada alumno 
    ///     cuando un profesor pide un listado 
    /// </summary>
    public class SchoolClassStudent
    {
        public Student Student { get; set; }
        public int SchoolClassId { get; set; }
        public Absence Absence { get; set; }     
    }
}
