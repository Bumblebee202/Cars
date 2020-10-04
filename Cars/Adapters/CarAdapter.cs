using Cars.Database;
using Cars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Adapters
{
    public class CarAdapter : ICarAdapter
    {
        IDatabase _database;

        public CarAdapter(IDatabase database)
        {
            _database = database;
            _database.SetConnectionString("Server=MSI;Database=Cars;Trusted_Connection=True;");
        }

        public async Task Create(string id, string name, string description)
        {
            await _database.OpenConnection();
            ITransaction transaction = _database.BeginTransaction();
            try
            {
                
                IProcedure procedure = _database.Procedure("CreateCar", transaction)
                                                .Parameter("ID", id)
                                                .Parameter("Name", name)
                                                .Parameter("Description", description);

                await _database.Execute(procedure);
                await _database.EndTransaction(transaction, false);
            }
            catch (Exception)
            {
                await _database.EndTransaction(transaction, true);
                throw;
            }
            finally
            {
                _database.CloseConnection();
            }
        }

        public async Task Delete(string id)
        {
            await _database.OpenConnection();
            ITransaction transaction = _database.BeginTransaction();
            try
            {

                IProcedure procedure = _database.Procedure("DeleteCar", transaction)
                                                .Parameter("ID", id);

                await _database.Execute(procedure);
                await _database.EndTransaction(transaction, false);
            }
            catch (Exception)
            {
                await _database.EndTransaction(transaction, true);
                throw;
            }
            finally
            {
                _database.CloseConnection();
            }
        }

        public async Task<IEnumerable<Car>> Enumerate()
        {
            await _database.OpenConnection();
            try
            {
                IProcedure procedure = _database.Procedure("EnumerateCar");
                return await _database.Fill<Car>(procedure);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _database.CloseConnection();
            }
        }

        public async Task<Car> Get(string id)
        {
            await _database.OpenConnection();
            try
            {
                IProcedure procedure = _database.Procedure("EnumerateCar");
                IEnumerable<Car> cars = await _database.Fill<Car>(procedure);
                return cars.Single();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _database.CloseConnection();
            }
        }

        public async Task Update(string id, string name, string description)
        {
            await _database.OpenConnection();
            ITransaction transaction = _database.BeginTransaction();
            try
            {

                IProcedure procedure = _database.Procedure("UpdateCar", transaction)
                                                .Parameter("ID", id)
                                                .Parameter("Name", name)
                                                .Parameter("Description", description);

                await _database.Execute(procedure);
                await _database.EndTransaction(transaction, false);
            }
            catch (Exception)
            {
                await _database.EndTransaction(transaction, true);
                throw;
            }
            finally
            {
                _database.CloseConnection();
            }
        }
    }
}
