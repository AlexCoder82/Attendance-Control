using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Exceptions
{
    public class CourseSubjectDuplicateEntryException : Exception
    {
        public static readonly string MESSAGE = "El curso ya tiene la asignatura asignada.";

        public CourseSubjectDuplicateEntryException() : base(MESSAGE)
        {

        }
    }
}
