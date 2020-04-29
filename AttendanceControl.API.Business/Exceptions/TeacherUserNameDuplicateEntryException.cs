using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Exceptions
{
    public class TeacherUserNameDuplicateEntryException : Exception
    {
        public static readonly string MESSAGE = "El nombre de usuario ya es utilizado por otra persona.";

        public TeacherUserNameDuplicateEntryException() : base(MESSAGE)
        {

        }
    }
}
