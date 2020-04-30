
using System;

namespace AttendanceControl.API.Business.Exceptions
{
    /// <summary>
    ///     Excepcion instanciada cuando un administrador envia 
    ///     credenciales erróneas
    /// </summary>
    public class WrongCredentialsException : Exception
    {
        public static readonly string MESSAGE = "Credenciales incorrectas.";
        
        public WrongCredentialsException():base(MESSAGE)
        {

        }
    }
}