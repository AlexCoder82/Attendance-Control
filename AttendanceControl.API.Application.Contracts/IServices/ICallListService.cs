using AttendanceControl.API.Application.Contracts.DTOs;
using AttendanceControl.API.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    public interface ICallListService
    {
        public Task<List<SchoolClassStudent>> Get(int[] schoolClassIds);
        public Task<bool> Post(List<SchoolClassStudent> callList);
    }
}
