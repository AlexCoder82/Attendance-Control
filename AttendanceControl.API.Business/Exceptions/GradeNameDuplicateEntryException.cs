using System;

namespace AttendanceControl.API.Business.Exceptions
{
    /// <summary>
    ///     Excepcion instanciada cuando se intenta insertar en la base 
    ///     de datos , un ciclo formativo cuyo nombre ya existe
    /// </summary>
    public class GradeNameDuplicateEntryException : Exception
    {
        public static readonly string MESSAGE = "Ya existe un grado con el mismo nombre.";

        public GradeNameDuplicateEntryException() : base(MESSAGE)
        {

        }
    
    }
}
