﻿using System;

namespace AttendanceControl.API.Business.Exceptions
{
    /// <summary>
    ///     Excepcion instanciada cuando se intenta insertar en la base 
    ///     de datos una asignatura cuyo nombre ya existe
    /// </summary>
    public class SubjectNameDuplicateEntryException : Exception
    {
        public static readonly string MESSAGE = "Ya existe una asignatura con el mismo nombre.";

        public SubjectNameDuplicateEntryException() : base(MESSAGE)
        {

        }

    }
}
