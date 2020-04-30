using System;

namespace AttendanceControl.API.Business.Exceptions
{
    /// <summary>
    ///     Excepcion instanciada cuando se intenta insertar en la base de datos
    ///     un profesor con un dni que ya existe
    /// </summary>
    public class DniDuplicateEntryException : Exception
    {
        public static readonly string MESSAGE = "Ya existe un profesor con el mismo dni.";

        public DniDuplicateEntryException() : base(MESSAGE)
        {

        }
    }
}
