using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace Cars.Database
{
    public class MsSql : IDatabase, IDisposable
    {
        bool _disposed;
        string _connectionString;
        SqlConnection _connection;

        MsSql() => _disposed = false;

        public void SetConnectionString(string connectionString)
        {
            Dispose(false);
            _connectionString = connectionString;
        }

        public async Task OpenConnection()
        {
            _connection = new SqlConnection(_connectionString);
            await _connection.OpenAsync();
        }

        public void CloseConnection() => Dispose(false);

        public ITransaction BeginTransaction()
        {
            ITransaction transaction = new MsSqlTransaction(_connection);
            return transaction;
        }

        public Task EndTransaction(ITransaction transaction, bool rollback)
        {
            return Task.Run(() =>
            {
                if (!rollback)
                    transaction.Commit();
                else
                    transaction.Rollback();
            });
        }

        public IProcedure Procedure(string name) => new MsSqlProcedure(name, _connection);

        public IProcedure Procedure(string name, ITransaction transaction) => new MsSqlProcedure(name, _connection, transaction);

        public async Task Execute(IProcedure procedure)
        {
            DbCommand command = procedure.Command;
            await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<T>> Fill<T>(IProcedure procedure)
        {
            DbCommand command = procedure.Command;
            await command.ExecuteNonQueryAsync();

            List<T> ts = new List<T>();

            DbDataReader reader = await command.ExecuteReaderAsync();

            Type type = typeof(T);

            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);

            PropertyInfo[] properties = type.GetProperties();

            while (await reader.ReadAsync())
            {
                object t = constructor.Invoke(null);

                foreach (var propertie in properties)
                    propertie.SetValue(t, reader[$"{propertie.Name}"]);

                ts.Add((T)t);
            }
            return ts;
        }

        protected async virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MsSql() => Dispose(false);
    }
}
