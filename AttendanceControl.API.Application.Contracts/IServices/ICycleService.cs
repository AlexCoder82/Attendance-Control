using AttendanceControl.API.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    public interface ICycleService
    {
        public Task<bool> Save(Cycle grade);

        public Task<List<Cycle>> GetAll();
    }
}
