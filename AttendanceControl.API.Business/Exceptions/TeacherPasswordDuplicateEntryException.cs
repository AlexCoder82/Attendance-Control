using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Exceptions
{
    public class TeacherPasswordDuplicateEntryException : Exception
    {
        public static readonly string MESSAGE = "La contraseña ya es utilizada por otra persona.";

        public TeacherPasswordDuplicateEntryException() : base(MESSAGE)
        {

        }
    }
}
