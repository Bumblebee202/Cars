using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Cars.Database
{
    public class MsSqlProcedure : IProcedure
    {
        readonly SqlCommand _command;

        public MsSqlProcedure(string name, SqlConnection connection)
        {
            _command = new SqlCommand(name, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
        }

        public MsSqlProcedure(string name, SqlConnection connection, ITransaction transaction) : this(name, connection)
            => _command.Transaction = transaction.Transaction as SqlTransaction;

        public DbCommand Command
        {
            get => _command;
        }

        public IProcedure Parameter<T>(string name, T value)
        {
            SqlDbType type = typeof(T).Name switch
            {
                "Byte" => SqlDbType.TinyInt,
                "Int16" => SqlDbType.SmallInt,
                "Int32" => SqlDbType.Int,
                "Int64" => SqlDbType.BigInt,
                "Single" => SqlDbType.Real,
                "Double" => SqlDbType.Float,
                "Decimal" => SqlDbType.Decimal,
                "String" => SqlDbType.NVarChar,
                "Boolean" => SqlDbType.Bit,
                "DateTime" => SqlDbType.DateTime,
                _ => throw new Exception("Error")
            };

            SqlParameter param = new SqlParameter
            {
                ParameterName = $"@{name}",
                Value = value,
                SqlDbType = type,
                //Direction = ParameterDirection.Input
            };

            _command.Parameters.Add(param);
            return this;
        }
    }
}
