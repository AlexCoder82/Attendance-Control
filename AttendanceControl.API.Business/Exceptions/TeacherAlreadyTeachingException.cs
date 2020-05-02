using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Exceptions
{
    /// <summary>
    ///     Excepcion instanciada cuando se intenta insertar en la base 
    ///     de datos una asignatura cuyo nombre ya existe
    /// </summary>
    public class TeacherAlreadyTeachingException : Exception
    { 

        public TeacherAlreadyTeachingException(string message) : base(message)
        {
            
        }

    }
}
