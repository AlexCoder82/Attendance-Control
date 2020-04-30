using System;

namespace AttendanceControl.API.Business.Exceptions
{
    /// <summary>
    ///     Excepcion instanciada cuando se intenta acceder a un
    ///     registro de la bases de datos que no existe
    /// </summary>
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string message) : base(message)
        {

        }
    }
}
