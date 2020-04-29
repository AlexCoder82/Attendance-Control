using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Business.Exceptions
{
    public class TeacherAlreadyRegistered : Exception
    {
        public static readonly string MESSAGE = "Tu dni ya esta registrado.";

        public TeacherAlreadyRegistered() : base(MESSAGE)
        {


        }
    }
}
