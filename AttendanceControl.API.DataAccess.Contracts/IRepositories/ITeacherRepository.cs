using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface ITeacherRepository: IRepository<TeacherEntity>
    {
        
    }
}
