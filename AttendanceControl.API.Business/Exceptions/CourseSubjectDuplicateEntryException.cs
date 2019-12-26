using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Exceptions
{
    public class CourseSubjectDuplicateEntryException : Exception
    {
        public static readonly string MESSAGE = "El courso tiene asignaturas repetidas.";

        public CourseSubjectDuplicateEntryException() : base(MESSAGE)
        {

        }
    }
}
