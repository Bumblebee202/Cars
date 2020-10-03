using System.Data.Common;
using System.Threading.Tasks;

namespace Cars.Database
{
    public interface ITransaction
    {
        Task Commit();
        Task Rollback();

        DbTransaction Transaction { get; }
    }
}
