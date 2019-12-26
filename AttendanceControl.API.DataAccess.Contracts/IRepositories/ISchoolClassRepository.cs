using AttendanceControl.API.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface ISchoolClassRepository: IRepository<SchoolClassEntity>
    {
        public Task<List<SchoolClassEntity>> GetByCourse(int courseId);

        public Task<SchoolClassEntity> Save(SchoolClassEntity schoolClassEntity);

        public  Task<bool> SetNotCurrent(int id);
    }
}
