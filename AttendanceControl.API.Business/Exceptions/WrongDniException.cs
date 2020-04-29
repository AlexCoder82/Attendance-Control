using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Exceptions
{
    public class WrongDniException : Exception
    {
        public static readonly string MESSAGE = "El sistema no reconoce tu Dni, prueba otra vez o consulta con el administrador.";

        public WrongDniException() : base(MESSAGE)
        {

        }


    }
}
