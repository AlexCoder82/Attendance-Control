using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.Application.Mappers
{
    public class SchoolClassMapper
    {
        public static SchoolClass Map(SchoolClassEntity schoolClassEntity)
        {
            return new SchoolClass()
            {
              

            };
        }

        public static SchoolClassEntity Map(SchoolClass schoolClass)
        {
            return new SchoolClassEntity()
            {
               


            };
        }
    }
}
