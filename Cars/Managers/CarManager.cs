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

        public async Task Delete(string id) => await _carAdapter.Delete(id);

        public async Task<IEnumerable<Car>> Enumerate() => await _carAdapter.Enumerate();

        public async Task<Car> Get(string id) => await _carAdapter.Get(id);

        //public async Task<Car> PartialUpdate(string id, string name)
        {
            Car car = await _carAdapter.Get(id);
            return await _carAdapter.Update(id, name, car.Description);
        }

        public async Task<Car> Save(string id, string name, string description)
        {
            if (id == null)
                return await _carAdapter.Create(name, description);
            else
               return await _carAdapter.Update(id, name, description);
        }

    }
}
