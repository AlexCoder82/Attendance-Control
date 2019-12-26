using AttendanceControl.API.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    public interface ISchoolClassService
    {
        public Task<SchoolClass> Save( SchoolClass schoolClass);

        public Task<bool> SetNotCurrent(int id);
        public Task<SchoolClass> Update(int courseId, SchoolClass schoolClass);
    }
}
