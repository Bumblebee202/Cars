using Cars.Adapters;
using Cars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Managers
{
    public class CarManager : ICarManager
    {
        ICarAdapter _carAdapter;

        public CarManager(ICarAdapter carAdapter) => _carAdapter = carAdapter;

        public async Task<Car> Create(string name, string description)
        {
            string id;
            Car car = await GetLast();
            if (car == null)
            {
                id = "1";
            }
            else
            {
                int newId = int.Parse(car.Id);
                id = $"{newId + 1}";
            }
            await _carAdapter.Create(id, name, description);
            return await GetLast();
        }

        public async Task Delete(string id) => await _carAdapter.Delete(id);

        public async Task<IEnumerable<Car>> Enumerate() => await _carAdapter.Enumerate();

        public async Task<Car> Get(string id) => await _carAdapter.Get(id);

        public async Task<Car> GetLast()
        {
            IEnumerable<Car> cars = await Enumerate();
            return cars.LastOrDefault();
        }

        public async Task<Car> Update(string id, string name, string description)
        {
            await _carAdapter.Update(id, name, description);
            return await _carAdapter.Get(id);
        }
    }
}
