using System.Data.Common;

namespace Cars.Database
{
    public interface IProcedure
    {
        IProcedure Parameter<T>(string name, T value);

        DbCommand Command { get; }
    }
}
