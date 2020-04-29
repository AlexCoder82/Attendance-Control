using AttendanceControl.API.Application.Contracts.DTOs;
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

        public Task<bool> Cancel(int schoolClassId);
     
        public Task<List<SchoolClass>> GetByCourse(int courseId);

        public Task<List<SchoolClass>> GetByTeacher(int teacherId);

    }
}
