using System;

namespace AttendanceControl.API.Business.Exceptions
{
    /// <summary>
    ///     Excepcion instanciada cuando la base de datos devuelve 
    ///     un error de entrada al querer asignarle a un curso
    ///     una asignatura que ya tiene asignada
    /// </summary>
    public class CourseSubjectDuplicateEntryException : Exception
    {
        public static readonly string MESSAGE = "El curso ya tiene la asignatura asignada.";

        public CourseSubjectDuplicateEntryException() : base(MESSAGE)
        {

        }
    }
}
