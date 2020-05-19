using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    /// <summary>
    ///     Contratos del repositorio de profesores
    /// </summary>
    public interface ITeacherRepository
    {

        public Task<TeacherEntity> Get(int id);
        public Task<TeacherEntity> GetByDni(string dni);
        public Task<List<TeacherEntity>> GetAll();
        public Task<TeacherEntity> Save(TeacherEntity entity);
        public Task<TeacherEntity> Update(TeacherEntity entity);

    }
}
