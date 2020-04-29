using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface IDatabaseTransaction
    {
        public Task Begin();
        public void Commit();
        public void Rollback();
    }
}
