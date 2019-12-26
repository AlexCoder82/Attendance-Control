using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Exceptions
{
    public class DniDuplicateEntryException : Exception
    {
        public static readonly string MESSAGE = "Ya existe un profesor con el mismo DNI.";

        public DniDuplicateEntryException() : base(MESSAGE)
        {

        }
    }
}
