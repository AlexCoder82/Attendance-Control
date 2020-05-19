using System;

namespace AttendanceControl.API.Business.Exceptions
{
    /// <summary>
    ///     Excepcion instanciada cuando un profesor intenta conectarse
    ///     al sistema usando un dni equivocado
    /// </summary>
    public class WrongDniException : Exception
    {
        public static readonly string MESSAGE = "No se reconoce tu dni.";

        public WrongDniException() : base(MESSAGE)
        {

        }

    }
}
