using System.Collections.Generic;
using System.Threading.Tasks;


namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface IRepository<T> where T : class
    {
        public Task<List<T>> GetAll();

        public Task<T> Get(int id);

        public Task<T> Save(T entity);

        public Task<bool> Delete(int id);

        public Task<T> Update(T entity);
    }
}
