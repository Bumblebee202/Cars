using Cars.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars.Managers
{
    public interface ICarManager
    {
        Task<Car> Save(string id, string name, string description);
        //Task<Car> PartialUpdate(string id, string name);
        Task Delete(string id);
        Task<Car> Get(string id);
        Task<IEnumerable<Car>> Enumerate();
    }
}
