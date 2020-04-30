using AttendanceControl.API.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    /// <summary>
    ///  Contratos de la lógica relacionada con los ciclos formativos
    /// </summary>
    public interface ICycleService
    {
        public Task<Cycle> Save(Cycle cycle);
        public Task<List<Cycle>> GetAll();
        public Task<bool> Update(Cycle cycle);
    }
}
