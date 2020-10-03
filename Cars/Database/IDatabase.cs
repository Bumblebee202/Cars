using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.Database
{
    public interface IDatabase
    {
        void SetConnectionString(string connectionString);
        Task OpenConnection();
        void CloseConnection();
        ITransaction BeginTransaction();
        Task EndTransaction(ITransaction transaction, bool rollback);
        IProcedure Procedure(string name);
        IProcedure Procedure(string name, ITransaction transaction);
        Task Execute(IProcedure procedure);
        Task<IEnumerable<T>> Fill<T>(IProcedure procedure);
    }
}
