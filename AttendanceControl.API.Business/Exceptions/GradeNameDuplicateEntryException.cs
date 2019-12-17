using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Exceptions
{
    public class GradeNameDuplicateEntryException : Exception
    {
        public static readonly string MESSAGE = "Ya existe un grado con el mismo nombre.";

        public GradeNameDuplicateEntryException() : base(MESSAGE)
        {

        }
    
    }
}
