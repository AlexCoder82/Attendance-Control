using AttendanceControl.API.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    public interface ITeacherService
    {
        public Task<List<Teacher>> GetAll();

        public Task<Teacher> Update(Teacher teacher);
        public Task<Teacher> Save(Teacher teacher);
    }
}
