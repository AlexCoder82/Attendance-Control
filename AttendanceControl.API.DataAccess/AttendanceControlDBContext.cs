using AttendanceControl.API.DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess
{
    public class AttendanceControlDBContext: DbContext, IAttendanceControlDBContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
