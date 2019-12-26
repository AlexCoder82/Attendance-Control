using System;

namespace AttendanceControl.API.Business.Exceptions
{
    public class SubjectNameDuplicateEntryException : Exception
    {
        public static readonly string MESSAGE = "Ya existe una asignatura con el mismo nombre.";

        public SubjectNameDuplicateEntryException() : base(MESSAGE)
        {

        }

    }
}
