using System;

namespace AttendanceControl.API.Business.Exceptions
{
    /// <summary>
    ///     Excepcion instanciada cuando se intenta insertar en la base 
    ///     de datos , un ciclo formativo cuyo nombre ya existe
    /// </summary>
    public class CycleNameDuplicateEntryException : Exception
    {
        public static readonly string MESSAGE = "Ya existe un ciclo formativo con el mismo nombre.";

        public CycleNameDuplicateEntryException() : base(MESSAGE)
        {

        }
    
    }
}
