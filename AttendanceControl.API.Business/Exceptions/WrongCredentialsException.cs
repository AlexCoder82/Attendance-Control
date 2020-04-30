
using System;

namespace AttendanceControl.API.Business.Exceptions
{
    public class WrongCredentialsException : Exception
    {
        public static readonly string MESSAGE = "Credenciales incorrectas.";
        
        public WrongCredentialsException():base(MESSAGE)
        {

        }
    }
}