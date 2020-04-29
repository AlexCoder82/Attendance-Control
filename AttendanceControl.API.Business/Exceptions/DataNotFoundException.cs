using System;

namespace AttendanceControl.API.Business.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string message) : base(message)
        {

        }
    }
}
