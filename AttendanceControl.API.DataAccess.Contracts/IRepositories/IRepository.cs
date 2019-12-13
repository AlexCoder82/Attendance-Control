using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll();

        public Task<T> Get(int id);

        public Task<T> Save(T entity);

        public Task<T> Delete(int id);

        public Task<T> Update(int id, T entity);
    }
}
