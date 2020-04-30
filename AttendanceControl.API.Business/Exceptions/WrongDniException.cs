using System;

namespace AttendanceControl.API.Business.Exceptions
{
    /// <summary>
    ///     Excepcion instanciada cuando un profesor intenta conectarse
    ///     al sistema usando un dni equivocado
    /// </summary>
    public class WrongDniException : Exception
    {
        public static readonly string MESSAGE = "El sistema no reconoce tu Dni, " +
            "prueba otra vez o consulta con el administrador.";

        public WrongDniException() : base(MESSAGE)
        {

        }

    }
}
